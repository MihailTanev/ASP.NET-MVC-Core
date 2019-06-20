namespace FDMC.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using FDMC.Data;
    using FDMC.Web.Service.Contracts;

    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }
        public IActionResult Index()
        {            
            return View(this.homeService.GetAllCats());
        }       
    }
}
