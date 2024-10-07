using Microsoft.AspNetCore.Identity;

namespace CarSalesWebsite.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ProfileImageUrl { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
