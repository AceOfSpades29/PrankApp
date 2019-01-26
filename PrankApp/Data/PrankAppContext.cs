using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PrankApp.Models
{
    public class PrankAppContext : DbContext
    {
        public PrankAppContext (DbContextOptions<PrankAppContext> options)
            : base(options)
        {
        }

        public DbSet<PrankApp.Models.Prank> Prank { get; set; }
    }
}
