using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Noemi.Models
{
    public class NoemiContext : DbContext
    {
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Trial> Trials { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<NoemiContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}