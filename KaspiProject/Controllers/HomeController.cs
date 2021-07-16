using CoreApplication.Interfaces;
using KaspiProject.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KaspiProject.Controllers
{
    public class HomeController : Controller
    {
        private ICrawlerService _crawlerService;
        public static Logger _logger = LogManager.GetCurrentClassLogger();

        public HomeController(ICrawlerService crawlerService)
        {
            _crawlerService = crawlerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Parse()
        {
            try
            {
                await _crawlerService.Create();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.StackTrace);
            }
            return new RedirectResult("~/swagger");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
