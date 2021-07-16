using DomainCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreApplication.Interfaces
{
    public interface ICrawlerService
    {
        Task Create();
    }
}
