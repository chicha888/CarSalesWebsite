using System.ComponentModel.DataAnnotations.Schema;

namespace CarSalesWebsite.Models
{
    public class Car
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public long Price { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }
        public string BodyType { get; set; }
        public string Color { get; set; }
        public double EngineCapacity { get; set; }
        public string Description { get; set; }
        public bool IsSold { get; set; }
        public DateTime DateAdded { get; set; }
        public string Location { get; set; }

        public ICollection<CarImage> CarImages { get; set; }
    }
}
