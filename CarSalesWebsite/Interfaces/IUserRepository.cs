using CarSalesWebsite.Models;

namespace CarSalesWebsite.Interfaces
{
    public interface IUserRepository
    {
        public Task<AppUser> GetUserByIdAsync(string id);
        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();
    }
}
