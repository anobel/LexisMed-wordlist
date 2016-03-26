using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedEater
{
    class Program
    {
        static void Main(string[] args)
        {
            //Download a feed: http://feeds.plos.org/plosbiology/NewArticles
            //Parse it into some kind of feed object?
            BridgeToAsync().Wait();
        }

        public static async Task BridgeToAsync()
        {
            var plosDownloader = new PlosDownloader("http://feeds.plos.org/plosbiology/NewArticles", new List<string> {});
            await plosDownloader.GetArticleContents();
        }
    }
}
