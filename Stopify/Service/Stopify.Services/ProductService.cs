namespace Stopify.Services
{
    using AutoMapper;
    using Stopify.Data;
    using Stopify.Models;
    using Stopify.Services.Mapping;
    using Stopify.Services.Models;
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
            ProductType productTypeDb = context.ProductTypes
                .SingleOrDefault(productType => productType.Name == model.ProductType.Name);

            Product product = Mapper.Map<Product>(model);
            product.ProductType = productTypeDb;

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

        public IQueryable<ProductServiceModel> GetAllProducts()
        {
            return this.context.Products.To<ProductServiceModel>();
        }

        public IQueryable<ProductTypeServiceModel> GetAllProductTypes()
        {
            return this.context.ProductTypes.To<ProductTypeServiceModel>();
        }
    }
}
