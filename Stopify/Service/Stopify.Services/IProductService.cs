namespace Stopify.Services
{
    using Stopify.Web.InputModels;

    public interface IProductService
    {
        bool Create(CreateProductInputModel model)
    }
}
