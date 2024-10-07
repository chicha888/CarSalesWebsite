using CarSalesWebsite.Models;
using System.ComponentModel.DataAnnotations;

namespace CarSalesWebsite.ViewModels
{
    public class CreateNewCarVM
    {
        [MaxLength(50, ErrorMessage = "Make cannot be longer than 50 symbols")]
        public string Make { get; set; }

        [MaxLength(50, ErrorMessage = "Model cannot be longer than 50 symbols")]
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
        public bool IsSold { get; set; } = false;
        public string Location { get; set; }

        public List<IFormFile> CarImages { get; set; }
    }
}
