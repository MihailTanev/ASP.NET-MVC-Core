namespace Stopify.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Stopify.Web.InputModels;
    using System.Threading.Tasks;

    public class ProductController : AdminController
    {
        [HttpGet(Name = "Create")]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost(Name = "Create")]
        public async Task<IActionResult> Create(CreateProductInputModel model)
        {
            return this.View();
        }
    }
}