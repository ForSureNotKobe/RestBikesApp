using CourseRESTApp.Models;
using Microsoft.EntityFrameworkCore;

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