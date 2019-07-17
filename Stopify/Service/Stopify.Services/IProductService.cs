namespace Stopify.Services
{
    using Stopify.Services.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task<IQueryable<ProductTypeServiceModel>> GetAllProductTypes();
        Task <bool> Create(ProductServiceModel model);

        Task<bool> CreateProductType(ProductTypeServiceModel product);
    }
}
