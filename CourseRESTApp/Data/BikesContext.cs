using Microsoft.EntityFrameworkCore;
using CourseRESTApp.Models;

namespace CourseRESTApp.Data
{
    public class BikesContext : DbContext
    {
        public BikesContext(DbContextOptions<BikesContext> options)
            : base(options)
        {
        }

        public DbSet<Bike> Bike { get; set; }
    }
}