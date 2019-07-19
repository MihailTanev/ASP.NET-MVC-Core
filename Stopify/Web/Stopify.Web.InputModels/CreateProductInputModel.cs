namespace Stopify.Web.InputModels
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Stopify.Services.Mapping;
    using Stopify.Services.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateProductInputModel : IMapTo<ProductServiceModel>, IHaveCustomMappings
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime ManufacturedOn { get; set; }

        [Required]
        public IFormFile Picture { get; set; }

        [Required]
        public string ProductType { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CreateProductInputModel, ProductServiceModel>()
                .ForMember(d => d.ProductType, map => map.MapFrom(vm => new ProductTypeServiceModel { Name = vm.ProductType}));
        }
    }
}
