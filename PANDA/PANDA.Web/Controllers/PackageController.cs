namespace Panda.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Panda.Data;
    using Panda.Models;
    using Panda.Web.ViewModels;
    using System;
    using System.Globalization;
    using System.Linq;

    public class PackageController : Controller
    {
        private readonly PandaDbContext context;

        public PackageController(PandaDbContext context)
        {
            this.context = context;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            this.ViewData["Recipients"] = this.context.Users.ToList();
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CreatePackageViewModel addPackage)
        {
            Package package = new Package
            {
                Description = addPackage.Description,
                Recipient = this.context.Users.SingleOrDefault(user => user.UserName == addPackage.Recipients),
                ShippingAddress = addPackage.ShippingAddress,
                Weight = addPackage.Weight,
                Status = this.context.PackageStatuses.SingleOrDefault(status => status.Name == "Pending")
            };

            this.context.Packages.Add(package);
            this.context.SaveChanges();

            return this.Redirect("/Package/Pending");
        }

        [HttpGet("/Packages/Ship/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Ship(string id)
        {
            Package package = this.context.Packages.Find(id);
            package.Status = this.context.PackageStatuses.SingleOrDefault(status => status.Name == "Shipped");
            package.EstimatedDeliveryDate = DateTime.Now.AddDays(new Random().Next(20, 40));
            this.context.Update(package);
            this.context.SaveChanges();
            return this.Redirect("/Package/Shipped");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/Package/Deliver/{id}")]
        public IActionResult Deliver(string id)
        {
            Package package = this.context.Packages.Find(id);
            package.Status = this.context.PackageStatuses.SingleOrDefault(status => status.Name == "Shipped");
            this.context.Update(package);
            this.context.SaveChanges();
            return this.Redirect("/Package/Delivered");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Pending()
        {
            return this.View(context.Packages
                .Include(package=>package.Recipient)
                .Where(package=>package.Status.Name =="Pending")
                .ToList().Select(package=>
                {
                    return new PackagePendingViewModel
                    {
                        Id = package.Id,
                        Description = package.Description,
                        Weight=package.Weight,
                        ShippingAddress=package.ShippingAddress,
                        Recipients = package.Recipient.UserName,
                    };
                }).ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Shipped()
        {
            return this.View(context.Packages
                .Include(package => package.Recipient)
                .Where(package => package.Status.Name == "Shipped")
                .ToList().Select(package =>
                {
                    return new PackageShippedViewModel
                    {
                        Id = package.Id,
                        Description = package.Description,
                        Weight = package.Weight,
                        Recipients=package.Recipient.UserName,
                        EstimatedDeliveryDate = package.EstimatedDeliveryDate?.ToString("dd/MM/YY", CultureInfo.InvariantCulture)
                    };
                }).ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delivered()
        {
            return this.View(context.Packages
                .Include(package => package.Recipient)
                .Where(package => package.Status.Name == "Delivered" || package.Status.Name == "Acquired")
                .ToList().Select(package =>
                {
                    return new PackageDeliveredViewModel
                    {
                        Id = package.Id,
                        Description = package.Description,
                        Weight = package.Weight,
                        Recipients = package.Recipient.UserName,
                        ShippingAddress = package.ShippingAddress
                    };
                }).ToList());
        }
        [Authorize]
        [HttpGet("/Package/Acquire/{id}")]
        public IActionResult Acquire(string id)
        {
            Package package = this.context.Packages.Find(id);
            package.Status = this.context.PackageStatuses.SingleOrDefault(status => status.Name == "Acquired");
            this.context.Update(package);

            Receipt reciept = new Receipt
            {
                Fee = (decimal)(2.67 * package.Weight),
                IssuedOn = DateTime.Now,
                Package = package,
                Recipient = context.Users.SingleOrDefault(user => user.UserName == this.User.Identity.Name),
            };

            this.context.Receipts.Add(reciept);

            this.context.SaveChanges();

            return this.Redirect("/Package/Delivered");
        }

        [Authorize]
        [HttpGet("/Package/Details/{id}")]
        public IActionResult Details(string id)
        {
            Package package = this.context.Packages
                .Where(packageDb => packageDb.Id == id)
                .Include(packageDb => packageDb.Recipient)
                .Include(packageDb=>packageDb.Status)
                .SingleOrDefault();

            PackageDetailsViewModel model = new PackageDetailsViewModel
            {
                Description = package.Description,
                Weight=package.Weight,
                ShippingAddress=package.ShippingAddress,
                Status=package.Status.Name,
                Recipient=package.Recipient.UserName
            };
            if (package.Status.Name == "Pending")
            {
                model.EstimatedDeliveryDate = "N/A";
            }
            else if (package.Status.Name == "Shipped")
            {
                model.EstimatedDeliveryDate = package.EstimatedDeliveryDate?.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture);
            }
            else
            {
                model.EstimatedDeliveryDate = "Pending";
            }

            return this.View();
        }
    }
}