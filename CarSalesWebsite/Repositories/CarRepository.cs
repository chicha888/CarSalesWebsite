using CarSalesWebsite.Data;
using CarSalesWebsite.Interfaces;
using CarSalesWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace CarSalesWebsite.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context) 
        {
            _context = context;
        }

        public bool Add(Car car)
        {
            _context.Cars.Add(car);
            return Save();
        }

        public bool Delete(Car car)
        {
            _context.Cars.Remove(car);
            return Save();
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _context.Cars.Include(u => u.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Save()
        {
            var result = _context.SaveChanges();
            return result > 0;
        }

        public bool Update(Car car)
        {
            _context.Cars.Update(car);
            return Save();
        }
    }
}
