using CoreApplication.Interfaces;
using DomainCore.Interfaces;
using DomainCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Services
{
    public class ArticleOperationService : IArticleOperationService
    {
        private IArticleOperationRepository _repository;
        public ArticleOperationService(IArticleOperationRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<Article> TextSearch(string text)
        {
            IEnumerable<Article> result = new List<Article>();
            try
            {
               result = _repository.TextSearch(text);
            }
            catch
            {
                throw;
            }
            return result;
        }

        public IEnumerable<Article> TimeIntervalSearch(DateTime from, DateTime to)
        {
            IEnumerable<Article> result = new List<Article>();
            try
            {
                result = _repository.TimeIntervalSearch(from, to);
            }
            catch
            {
                throw;
            }
            return result;
        }

        public Dictionary<string, int> TopTenWordsSearch()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            try
            {
                result = _repository.TopTenWordsSearch();
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
