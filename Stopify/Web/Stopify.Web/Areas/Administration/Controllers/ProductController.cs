namespace Stopify.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Stopify.Services.Models;
    using Stopify.Web.InputModels;
    using System.Threading.Tasks;

    public class ProductController : AdminController
    {
        [HttpGet]
        [Route("/Type/Create")]
        public async Task<IActionResult> CreateType()
        {
            return this.View("Type/Create");
        }

        
        [HttpPost]
        [Route("/Type/Create")]
        public async Task<IActionResult> CreateType(ProductTypeCreateInputModel model)
        {
            ProductTypeServiceModel product = new ProductTypeServiceModel
            {
                Name = model.Name
            };


            return this.Redirect("/");
        }


        [HttpGet(Name = "Create")]
        public async Task<IActionResult> Create()
        {
           return this.View();
        }

        [HttpPost(Name = "Create")]
        public async Task<IActionResult> Create(CreateProductInputModel model)
        {
            ProductServiceModel serviceModel = new ProductServiceModel
            {
                Name = model.Name,
                Price = model.Price,
                ManufacturedOn=model.ManufacturedOn,
                ProductType = new ProductTypeServiceModel { Name = model.ProductType },
            }; 

            return this.Redirect("/");
        }
    }
}