using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;

namespace Infrastruture
{
    public class PhoneBookContext:DbContext
    {
        public PhoneBookContext(DbContextOptions<PhoneBookContext> options):base(options)
        {

        }

        public DbSet<Contact> Contact { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().ToTable("contact");
            modelBuilder.Entity<Contact>().HasKey(c=>c.id);
        }
    }
}
