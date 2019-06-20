namespace FDMC.Web.Service.Contracts
{
    using FDMC.Web.ViewModels.Home;
    using System.Collections.Generic;
    public interface IHomeService
    {
        IList<CatViewModel> GetAllCats();
    }
}
