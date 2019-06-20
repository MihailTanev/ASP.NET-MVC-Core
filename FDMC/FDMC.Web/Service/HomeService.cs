namespace FDMC.Web.Service
{
    using FDMC.Data;
    using FDMC.Web.Service.Contracts;
    using FDMC.Web.ViewModels.Home;
    using System.Collections.Generic;
    using System.Linq;

    public class HomeService : IHomeService
    {
        private readonly FDMCDbContext context;

        public HomeService(FDMCDbContext context)
        {
            this.context = context;
        }
        public IList<CatViewModel> GetAllCats()
        {
            var indexViewModel = this.context
                .Cats
                .Select(x => new CatViewModel
                {
                    Id=x.Id,
                    CatName = x.Name

                }).ToList();

            return indexViewModel;
        }

    }
}
