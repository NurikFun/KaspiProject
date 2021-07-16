using DomainCore.Interfaces;
using DomainCore.Models;
using Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repositories
{
    public class ArticleOperationRepository : IArticleOperationRepository
    {
        public ArticleContext _context;
        public ArticleOperationRepository(ArticleContext context)
        {
            _context = context;
        }
        public IEnumerable<Article> TextSearch(string text)
        {
            var result = new List<Article>();
            try
            {
                result = _context.Articles.Where(x => x.Text.Contains(text)).ToList();
            }
            catch
            {
                throw;
            }
            return result;
        }

        public IEnumerable<Article> TimeIntervalSearch(DateTime from, DateTime to)
        {
            var result = new List<Article>();
            try
            {
                result = _context.Articles.Where(x => x.Time >= from && x.Time <= to)
                    .OrderByDescending(x => x.Time).ToList();
            }
            catch
            {
                throw;
            }
            return result;
        }

        public Dictionary<string, int> TopTenWordsSearch()
        {
            var result = new Dictionary<string, int>();
            try
            {
                string texts = string.Join(" ", _context.Articles.Select(x => x.Text)); // The maximum size of the String object in memory can be 2GB or about 1 billion characters.
                result = texts.Split(new char[] { ' ', ',', '.', ':', '\t', '-', '"', '\\' })
                    .GroupBy(x => x.ToLower())
                    .Select(x => new
                    {
                        Word = x.Key,
                        Count = x.Count()
                    })
                    .Where(x => !string.IsNullOrEmpty(x.Word))
                    .OrderByDescending(x => x.Count)
                    .Take(10).ToDictionary(x => x.Word, y => y.Count);
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
