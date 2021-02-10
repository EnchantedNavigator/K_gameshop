using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PepegaRequiem.Models
{
    public class PepegaContext : DbContext
    {

        public DbSet<Game> Games { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Developer> Developers { get; set; }

        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}