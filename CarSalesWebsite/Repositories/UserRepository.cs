using CarSalesWebsite.Data;
using CarSalesWebsite.Interfaces;
using CarSalesWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace CarSalesWebsite.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) 
        {
            _context = context;
        }

        public bool Add(AppUser user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            return await _context.Users.Include(x => x.Cars).ThenInclude(x => x.CarImages).FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }
    }
}
