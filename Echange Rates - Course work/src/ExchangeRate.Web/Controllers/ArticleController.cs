using ExchangeRate.Services.Contracts;
using ExchangeRate.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRate.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticlesService articlesService;

        public ArticleController(ArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public IActionResult Read()
        {
            return View();
        }
    }
}