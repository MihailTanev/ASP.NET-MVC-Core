namespace Panda.Web.ViewModels.Receipt
{
    public class ReceiptDetailsViewModel
    {
        public string Id { get; set; }
        public string DeliveryAddress { get; set; }
        public string IssuedOn { get; set; }
        public double PackageWeight { get; set; }
        public string PackageDescription { get; set; }
        public decimal Total { get; set; }
        public string Recipient { get; set; }
    }
}
