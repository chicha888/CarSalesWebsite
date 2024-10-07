using System.ComponentModel.DataAnnotations.Schema;

namespace CarSalesWebsite.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUser User { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
