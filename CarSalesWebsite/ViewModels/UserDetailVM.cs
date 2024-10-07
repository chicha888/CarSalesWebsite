using CarSalesWebsite.Models;

namespace CarSalesWebsite.ViewModels
{
    public class UserDetailVM
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ProfileImageUrl { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public ICollection<Car> Cars { get; set; }
        
    }
}
