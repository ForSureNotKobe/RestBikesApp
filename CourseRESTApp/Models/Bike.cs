using System;
using System.ComponentModel.DataAnnotations;

namespace CourseRESTApp.Models
{
    public class Bike
    {
        public int Id { get; set; }
        [StringLength(20, MinimumLength = 1)]
        [Required]
        public string Brand { get; set; }

        [StringLength(20, MinimumLength = 1)]
        [Required]
        public string Model { get; set; }

        [Range(2010, 2030)]
        [Display(Name = "Production Year")]
        [Required]
        public int ProdYear { get; set; }

        [Range(10, 31)]
        [Display(Name = "Wheel Size")]
        [Required]
        public int WheelSize { get; set; }

        [Range(10, 23)]
        [Display(Name = "Frame Size")]
        [Required]
        public int FrameSize { get; set; }

        //TODO: While editting/adding new doesn't allow semicolon for data "It's not a number" FIX
        [DataType(DataType.Currency)]
        [Required]
        public decimal Price { get; set; }
    }
}