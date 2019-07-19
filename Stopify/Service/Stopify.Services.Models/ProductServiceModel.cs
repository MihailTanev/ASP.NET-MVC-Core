namespace Stopify.Services.Models
{
    using Stopify.Models;
    using Stopify.Services.Mapping;
    using System;

    public class ProductServiceModel : IMapFrom<Product>, IMapTo<Product>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public int ProductTypeId { get; set; }
        public ProductTypeServiceModel ProductType { get; set; }
        public decimal Price { get; set; }
        public DateTime ManufacturedOn { get; set; }
    }
}
