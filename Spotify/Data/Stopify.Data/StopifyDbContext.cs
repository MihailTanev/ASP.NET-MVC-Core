﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stopify.Models;

namespace Stopify.Data
{
    public class StopifyDbContext : IdentityDbContext<User>
    {
        public StopifyDbContext(DbContextOptions<StopifyDbContext> options)
            : base(options)
        {
        }
    }
}
