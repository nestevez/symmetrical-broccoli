using Microsoft.EntityFrameworkCore;
 
namespace chartreuse.Models
{
    public class chartreuseContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public chartreuseContext(DbContextOptions<chartreuseContext> options) : base(options) 
        {} 
        public DbSet<Person> users {get;set;}
    }
}