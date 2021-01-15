using CourseRESTApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CourseRESTApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BikesContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BikesContext>>()))
            {
                // Look for any movies.
                if (context.Bike.Any())
                {
                    return;   // DB has been seeded
                }

                context.Bike.AddRange(
                    new Bike
                    {
                        Brand = "Kross",
                        Model = "Hexagon 3.0",
                        ProdYear = 2019,
                        Price = 1999.99M,
                        WheelSize = 29,
                        FrameSize = 19
                    },

                    new Bike
                    {
                        Brand = "Merida",
                        Model = "Down Pipe",
                        ProdYear = 2020,
                        Price = 2599.99M,
                        WheelSize = 29,
                        FrameSize = 19
                    },

                    new Bike
                    {
                        Brand = "Indiana",
                        Model = "Explorer",
                        ProdYear = 2018,
                        Price = 1499.99M,
                        WheelSize = 27,
                        FrameSize = 17
                    },

                    new Bike
                    {
                        Brand = "RockRider",
                        Model = "ST 540",
                        ProdYear = 2021,
                        Price = 1799.99M,
                        WheelSize = 27,
                        FrameSize = 21
                    }
                );
                context.SaveChanges();
            }
        }
    }
}