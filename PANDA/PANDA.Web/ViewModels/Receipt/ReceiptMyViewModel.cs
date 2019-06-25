using System;

namespace Panda.Web.ViewModels.Receipt
{
    public class ReceiptMyViewModel
    {
        public string Id { get; set; }
        public decimal Fee { get; set; }
        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        public string Recipient { get; set; }

    }
}
