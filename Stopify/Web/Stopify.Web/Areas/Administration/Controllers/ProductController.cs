namespace Stopify.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Stopify.Services;
    using Stopify.Services.Models;
    using Stopify.Web.InputModels;
    using System.Threading.Tasks;

    public class ProductController : AdminController
    {
        private readonly IProductService productService;
        
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        [Route("/Administration/Product/Type/Create")]
        public async Task<IActionResult> CreateType()
        {
            return this.View("Type/Create");
        }

        
        [HttpPost]
        [Route("/Administration/Product/Type/Create")]
        public async Task<IActionResult> CreateType(ProductTypeCreateInputModel model)
        {
            ProductTypeServiceModel productType = new ProductTypeServiceModel
            {
                Name = model.Name
            };

            await this.productService.CreateProductType(productType);

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

            await this.productService.Create(serviceModel);

            return this.Redirect("/");
        }
    }
}