using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1
{
    public class ModelContext : DbContext
    {

        public DbSet<Game> Games { set; get; }
        public DbSet<Player> Players { set; get; }

    }
}
