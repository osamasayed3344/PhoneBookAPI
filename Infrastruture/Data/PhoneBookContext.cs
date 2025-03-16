using  Microsoft.EntityFrameworkCore;
using Domain.Entity;
namespace Infrastruture.Data;

public class PhoneBookContext:DbContext
{
    public DbSet<Contact> Contacts {set; get;}

    public PhoneBookContext(DbContextOptions options):base(options){
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>().ToTable("contact");
    }
}
