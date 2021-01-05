using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CourseRESTApp.Models
{
    public class BikeBrandViewModel
    {
        public List<Bike> Bikes { get; set; }
        public SelectList Brands { get; set; }
        public string BikeBrand { get; set; }
        public string SearchString { get; set; }
    }
}