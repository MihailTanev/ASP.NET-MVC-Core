namespace Stopify.Services
{
    using Stopify.Services.Models;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task <bool> Create(ProductServiceModel model);

        Task<bool> CreateProductType(ProductTypeServiceModel product);
    }
}
