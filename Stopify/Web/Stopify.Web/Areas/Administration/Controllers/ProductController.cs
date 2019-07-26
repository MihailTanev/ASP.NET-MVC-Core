namespace Stopify.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Stopify.Services;
    using Stopify.Services.Models;
    using Stopify.Web.InputModels;
    using System.Threading.Tasks;
    using System.Linq;
    using Stopify.Web.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;

    public class ProductController : AdminController
    {
        private readonly IProductService productService;
        private readonly ICloudinaryService cloudinaryService;


        public ProductController(IProductService productService, ICloudinaryService cloudinaryService)
        {
            this.productService = productService;
            this.cloudinaryService = cloudinaryService;
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
            var allProductTypes = await this.productService.GetAllProductTypes().ToListAsync();

            this.ViewData["types"] = allProductTypes.Select(productType => new ProductCreateProductTypeViewModel
            {
                Name = productType.Name
            }).ToList(); 

            return this.View();
        }

        [HttpPost(Name = "Create")]
        public async Task<IActionResult> Create(CreateProductInputModel model)
        {
            string pictureUrl = await this.cloudinaryService.UploadPictureAsync(model.Picture, model.Name);

            ProductServiceModel serviceModel = Mapper.Map<ProductServiceModel>(model);

            serviceModel.Picture = pictureUrl;

            await this.productService.Create(serviceModel);

            return this.Redirect("/");
        }
    }
}