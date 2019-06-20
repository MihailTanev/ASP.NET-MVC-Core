namespace FDMC.Web.Controllers
{
    using FDMC.Web.Service.CatServices;
    using FDMC.Web.ViewModels.Cat;
    using Microsoft.AspNetCore.Mvc;
    

    public class CatsController : Controller
    {
        private readonly ICatService catService;

        public CatsController(ICatService catService)
        {
            this.catService = catService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var addCatViewModel = new AddCatViewModel();
            return this.View(addCatViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCatViewModel addCat)
        {
            if(addCat != null && ModelState.IsValid)
            {
               this.catService.AddCat(addCat);
               return Redirect("/");
            }
            else
            {
                return this.View(addCat);
            }
            
        }

        public IActionResult Details(int id)
        {
            var cat = this.catService.GetCat(id);
            return this.View(cat);
        }
    }
}