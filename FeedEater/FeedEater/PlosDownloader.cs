using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace FeedEater
{
    internal class PlosDownloader
    {
        private readonly string _feedUrl;
        private readonly HashSet<string> _articleIds;
        public PlosDownloader(string feedUrl, IEnumerable<string> processedArticleIds)
        {
            _feedUrl = feedUrl;
            _articleIds = new HashSet<string>(processedArticleIds, StringComparer.OrdinalIgnoreCase);
        }

        public async Task<IEnumerable<DownloadedArticle>> GetArticleContents()
        {
            var relevantArticles = await GetRelevantArticleUrls();

            var downloads = new Task<string>[] {};
            try
            {
                downloads = relevantArticles.Values.Select(DownloadPage).ToArray();
                var result = await Task.WhenAll(downloads);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong" + e.Message);
                Console.WriteLine(e.StackTrace);
                return AggregateResults(relevantArticles, downloads);
            }
            return AggregateResults(relevantArticles, downloads);
        }

        internal static IEnumerable<DownloadedArticle> AggregateResults(Dictionary<string, string> relevantArticles, Task<string>[] downloads)
        {
            var aggregatedResults = new List<DownloadedArticle>(downloads.Length);
            var i = 0;
            foreach (var article in relevantArticles)
            {
                aggregatedResults.Add(new DownloadedArticle(downloads[i].Result, article.Key, article.Value));
                i++;
            }
            return aggregatedResults;
        }

        internal async Task<Dictionary<string,string>> GetRelevantArticleUrls()
        {
            var rawContent = await DownloadPage(_feedUrl);
            var stringReader = new StringReader(rawContent);
            var xmlReader = XmlReader.Create(stringReader);
            var feed = SyndicationFeed.Load(xmlReader);

            var relevantItems = feed.Items.Where(item => !_articleIds.Contains(item.Id)).ToList();

            var pairs = new Dictionary<string, string>(relevantItems.Count);
            foreach (var item in relevantItems)
            {
                var preferredUrl = item.Links
                    .Where(IsTextXml)
                    .Select(syndicationLink => syndicationLink.Uri.AbsoluteUri)
                    .First();

                pairs.Add(item.Id, preferredUrl);
            }
            return pairs;
        }

        internal static bool IsTextXml(SyndicationLink link)
        {
            var mediaType = link?.MediaType;
            return !string.IsNullOrWhiteSpace(mediaType) && mediaType.Equals("text/xml", StringComparison.OrdinalIgnoreCase);
        }

        internal async Task<string> DownloadPage(string url)
        {
            using (var client = new HttpClient())
            using (var response = await client.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                using (var content = response.Content)
                {
                    var result = await content.ReadAsStringAsync();
                    return result;
                }
            }
        }
    }
}
