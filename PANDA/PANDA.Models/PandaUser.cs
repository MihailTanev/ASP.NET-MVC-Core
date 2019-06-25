using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Panda.Models
{
    public class PandaUser : IdentityUser
    {
        public ICollection<Package> Packages { get; set; }
        public ICollection<Receipt> Receipts { get; set; }

        public PandaUser()
        {
            this.Packages = new List<Package>();
            this.Receipts = new List<Receipt>();
        }
    }
}
