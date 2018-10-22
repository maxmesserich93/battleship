using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace WebApplication1.Models
{
    public class Player
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public IList<Game> Games { set; get; }
        public DateTime JoinDate { set; get; }

        public class PlayerDBContext : DbContext
        {
            public DbSet<Game> Games { set; get; }

        }
    }
}