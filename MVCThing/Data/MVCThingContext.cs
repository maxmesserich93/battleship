using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCThing.Models;

namespace MVCThing.Models
{
    public class MVCThingContext : DbContext
    {
        public MVCThingContext (DbContextOptions<MVCThingContext> options)
            : base(options)
        {
        }

        public DbSet<MVCThing.Models.Game> Game { get; set; }

        public DbSet<MVCThing.Models.Player> Player { get; set; }
    }
}
