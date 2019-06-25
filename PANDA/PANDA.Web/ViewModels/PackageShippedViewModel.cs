namespace Panda.Web.ViewModels
{    public class PackageShippedViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public string Recipients { get; set; }

        public string EstimatedDeliveryDate { get; set; }
    }
}
