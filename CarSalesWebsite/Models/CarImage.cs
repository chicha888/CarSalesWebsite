using System.ComponentModel.DataAnnotations.Schema;

namespace CarSalesWebsite.Models
{
    public class CarImage
    {
        public int Id { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateUploaded { get; set; }
        public bool IsPrimary { get; set; }
    }
}
