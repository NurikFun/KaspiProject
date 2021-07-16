using CoreApplication.Interfaces;
using DomainCore.Interfaces;
using DomainCore.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoreApplication.Services
{
    public class TengriNewsCrawlerService : ICrawlerService
    {
        private ICrawlerRepository _repository;

        private const string _URL = "https://tengrinews.kz/";
        private int _pageCount = 2;
        public TengriNewsCrawlerService(ICrawlerRepository repository)
        {
            _repository = repository;
        }

        public async Task Create()
        {
            try
            {
                var articles = await StartParsingAsync();
                _repository.Create(articles);
            }
            catch
            {
                throw;
            }
        }

        private async Task<IEnumerable<Article>> StartParsingAsync()
        {
            IEnumerable<string> pages = new List<string>();
            try
            {
                pages = await GetArticleLinksAsync(_pageCount);
            }
            catch
            {
                throw;
            }
            List<Article> result = new List<Article>();
            foreach (var page in pages)
            {
                try
                {
                    var htmlDocument = await GetHtmlDocumentAsync(_URL + page);
                    var title = htmlDocument.DocumentNode.SelectNodes("//*[contains(@class,'tn-content-title')]").FirstOrDefault();
                    var unorderedList = htmlDocument.DocumentNode.SelectNodes("//*[contains(@class,'tn-data-list')]").FirstOrDefault();
                    var paragraphs = htmlDocument.DocumentNode.SelectNodes("//*[contains(@class,'tn-news-text')]").FirstOrDefault();
                    var text = string.Join(" ", paragraphs.Descendants("p").Select(x => x.InnerText));
                    var time = unorderedList.Descendants("li").FirstOrDefault().InnerText;
                    text = Regex.Replace(text, @"<[^>]+>|&nbsp;", "").Trim();
                    result.Add(new Article
                    {
                        Title = title.InnerText.Replace("&quot;", string.Empty),
                        Time = GetCorrectDate(time.Trim()),
                        Text = Regex.Replace(text, @"\s{2,}", " ")
                    });
                }
                catch
                {
                    //log
                    throw;
                }

            }
            return result;
        }
        private async Task<IEnumerable<string>> GetArticleLinksAsync(int count)
        {
            List<string> links = new List<string>();
            for (int i = 1; i <= count; i++)
            {
                var page = _URL + "read/page/" + i;
                try
                {
                    var htmlDocument = await GetHtmlDocumentAsync(page);
                    if (htmlDocument != null)
                    {
                        var divs = htmlDocument.DocumentNode
                            .SelectNodes("//*[contains(@class,'tn-article-grid')]")
                            .FirstOrDefault();

                        links.AddRange(divs.Descendants("a")
                            .Select(x => x.ChildAttributes("href").FirstOrDefault().Value)
                            .ToList());
                    }
                }
                catch
                {
                    throw;
                }
            }
            return links;
        }

        private async Task<HtmlDocument> GetHtmlDocumentAsync(string page)
        {
            HtmlDocument htmlDocument = null;
            try
            {
                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(page);
                htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
            }
            catch
            {
                throw;
            }
            return htmlDocument;
        }

        private DateTime GetCorrectDate(string date)
        {
            try
            {
                DateTime _date = new DateTime();
                int index = date.IndexOf(",");
                if (index >= 0)
                    date = date.Substring(0, index);

                if (DateTime.TryParseExact(date, "dd MMMM yyyy", new CultureInfo("ru-RU"),
                                          DateTimeStyles.None, out _date))
                {
                    return _date.Date;
                }
                else if (date.Contains("сегодня"))
                {
                    return DateTime.Now.Date;
                }
                return DateTime.Now.AddDays(-1).Date;
            }
            catch
            {
                throw;
            }
        }

    }
}
