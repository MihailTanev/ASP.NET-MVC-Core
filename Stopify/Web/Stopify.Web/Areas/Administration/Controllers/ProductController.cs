namespace Stopify.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ProductController : AdminController
    {
        [HttpPost(Name = "Create")]
        public async Task<IActionResult> Create()
        {
            return null;
        }
    }
}