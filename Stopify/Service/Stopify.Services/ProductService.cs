namespace Stopify.Services
{
    using Microsoft.EntityFrameworkCore;
    using Stopify.Data;
    using Stopify.Models;
    using Stopify.Services.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly StopifyDbContext context;

        public ProductService(StopifyDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(ProductServiceModel model)
        {
            Product product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                ManufacturedOn = model.ManufacturedOn,
            };

            context.Products.Add(product);
            int result = await context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> CreateProductType(ProductTypeServiceModel product)
        {
            ProductType type = new ProductType
            {
                Name = product.Name,
            };

            context.ProductTypes.Add(type);
            int result = await context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<ProductTypeServiceModel>> GetAllProductTypes()
        {
            return await this.context.ProductTypes
                .Select(productType => new ProductTypeServiceModel
                {
                    Id = productType.Id,
                    Name = productType.Name
                }).ToListAsync();
        }   
    }
}
