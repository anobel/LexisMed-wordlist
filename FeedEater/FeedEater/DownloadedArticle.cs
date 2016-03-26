using System.Collections.Generic;

namespace FeedEater
{
    internal class DownloadedArticle
    {
        public string FullText { get; private set; }
        public string Id { get; private set; }
        public string Url { get; private set; }

        public DownloadedArticle(string fullText, string id, string url)
        {
            FullText = fullText;
            Id = id;
            Url = url;
        }
    }
}
