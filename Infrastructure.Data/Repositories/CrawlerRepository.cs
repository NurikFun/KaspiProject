using DomainCore.Interfaces;
using DomainCore.Models;
using Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repositories
{
    public class CrawlerRepository : ICrawlerRepository
    {
        public ArticleContext _context;
        public CrawlerRepository(ArticleContext context)
        {
            _context = context;
        }
        public void Create(IEnumerable<Article> articles)
        {
            using (_context)
            {
                try
                {
                    _context.Articles.AddIfNotExists(x => x.Title, articles.ToArray());
                    _context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
