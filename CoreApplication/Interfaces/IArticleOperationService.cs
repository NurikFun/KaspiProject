using DomainCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Interfaces
{
    public interface IArticleOperationService
    {
        IEnumerable<Article> TimeIntervalSearch(DateTime from, DateTime to);
        Dictionary<string, int> TopTenWordsSearch();
        IEnumerable<Article> TextSearch(string text);
    }
}
