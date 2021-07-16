using CoreApplication.Interfaces;
using CoreApplication.Services;
using DomainCore.Interfaces;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICrawlerService, TengriNewsCrawlerService>();
            services.AddScoped<ICrawlerRepository, CrawlerRepository>();
            services.AddScoped<IArticleOperationService, ArticleOperationService>();
            services.AddScoped<IArticleOperationRepository, ArticleOperationRepository>();
        }
    }
}
