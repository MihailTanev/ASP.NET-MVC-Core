namespace FDMC.Web.Service.CatServices
{
    using FDMC.Data;
    using FDMC.Model;
    using FDMC.Web.ViewModels.Cat;
    using System.Linq;

    public class CatService : ICatService
    {
        private readonly FDMCDbContext context;

        public CatService(FDMCDbContext context)
        {
            this.context = context;
        }
        public void AddCat(AddCatViewModel c)
        {
            var cat = new Cat
            {
                Name = c.Name,
                Age = c.Age,
                Breed = c.Breed,
                ImageUrl = c.ImageUrl
            };
            this.context.Cats.Add(cat);
            this.context.SaveChanges();
        }

        public CatDetailsViewModel GetCat(int id)
        {
            var catDetailViewModel = this.context.Cats
                .Where(x => x.Id == id)
                .Select(x => new CatDetailsViewModel
                {
                    Id = x.Id,
                    CatAge = x.Age,
                    CatBreed = x.Breed,
                    CatName = x.Name,
                    CatImageUrl = x.ImageUrl
                })
                .FirstOrDefault();

            return catDetailViewModel;
        }
    }
}
