namespace Stopify.Services
{
    using Stopify.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task<IEnumerable<ProductTypeServiceModel>> GetAllProductTypes();
        Task <bool> Create(ProductServiceModel model);

        Task<bool> CreateProductType(ProductTypeServiceModel product);
    }
}
