namespace Panda.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Panda.Data;
    using Panda.Web.ViewModels.Receipt;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Panda.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;

    public class ReceiptController : Controller
    {
        private readonly PandaDbContext context;

        public ReceiptController(PandaDbContext context)
        {
            this.context = context;
        }

        [Authorize]
        public IActionResult My()
        {
            List<ReceiptMyViewModel> myReceipts = this.context.Receipts
                .Include(receipt => receipt.Recipient)
                .Where(receipt => receipt.Recipient.UserName == this.User.Identity.Name)
                .Select(receipt => new ReceiptMyViewModel
                {
                    Id=receipt.Id,
                    Fee=receipt.Fee,
                    IssuedOn = receipt.IssuedOn,
                    Recipient=receipt.Recipient.UserName
                })
                .ToList();                

            return View(myReceipts);
        }

        [HttpGet("/Receipts/Details/{id}")]
        [Authorize]
        public IActionResult Details(string id)
        {
            Receipt receiptFromDb = this.context.Receipts
                .Where(receipt => receipt.Id == id)
                .Include(receipt => receipt.Package)
                .Include(receipt => receipt.Recipient)
                .SingleOrDefault();

            ReceiptDetailsViewModel viewModel = new ReceiptDetailsViewModel
            {
                Id = receiptFromDb.Id,
                Total = receiptFromDb.Fee,
                Recipient = receiptFromDb.Recipient.UserName,
                DeliveryAddress = receiptFromDb.Package.ShippingAddress,
                PackageWeight = receiptFromDb.Package.Weight,
                PackageDescription = receiptFromDb.Package.Description,
                IssuedOn = receiptFromDb.IssuedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
            };

            return this.View(viewModel);
        }
    }
}