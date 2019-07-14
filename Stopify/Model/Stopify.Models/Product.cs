namespace Stopify.Models
{
    using System;

    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public decimal Price { get; set; }
        public DateTime ManufacturedOn { get; set; }
    }
}
