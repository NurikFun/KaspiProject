using CoreApplication.Interfaces;
using DomainCore.Models;
using KaspiProject.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KaspiProject.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ArticleController : ControllerBase
    {
        private IArticleOperationService _articleService;
        public static Logger _logger = LogManager.GetCurrentClassLogger();

        public ArticleController(IArticleOperationService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("posts")]
        public IActionResult TimeIntervalSearch(DateTime from, DateTime to)
        {

            List<Article> result = new List<Article>();
            try
            {
                result = _articleService.TimeIntervalSearch(from, to).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = "Error occured watch in Log" });
            }

            return Ok(result);
        }
        [HttpGet("search")]
        public IActionResult TextSearch(string text)
        {
            List<Article> result = new List<Article>();
            try
            {
                result = _articleService.TextSearch(text).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = "Error occured watch in Log" });
            }

            return Ok(result);
        }

        [HttpGet("topten")]
        public IActionResult GetTopWords()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            try
            {
                result = _articleService.TopTenWordsSearch();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = "Error occured watch in Log" });
            }
            return Ok(result);
        }

    }
}
