using FDMC.Web.ViewModels.Cat;

namespace FDMC.Web.Service.CatServices
{
    public interface ICatService
    {
        void AddCat(AddCatViewModel c);
        CatDetailsViewModel GetCat(int id);

    }
}
