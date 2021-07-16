using DomainCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainCore.Interfaces
{
    public interface ICrawlerRepository
    {
        void Create(IEnumerable<Article> articles);
    }
}
