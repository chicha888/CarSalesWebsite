using CarSalesWebsite.Data;
using CarSalesWebsite.Interfaces;
using CarSalesWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace CarSalesWebsite.Repositories
{
    public class CarImageRepository : ICarImageRepository
    {
        private readonly AppDbContext _context;

        public CarImageRepository(AppDbContext context) 
        { 
            _context = context;
        }

        public bool Add(CarImage carImage)
        {
            _context.CarImages.Add(carImage);
            return Save();
        }

        public bool Delete(CarImage carImage)
        {
            _context.CarImages.Remove(carImage);
            return Save();
        }

        public async Task<List<CarImage>> GetImageFullInfoByIdAsync(int carId)
        {
            var images = await _context.CarImages.Where(x => x.CarId == carId).ToListAsync();
            return images;
        }

        public async Task<List<string>> GetCarImagesByIdAsync(int carId)
        {
            var images = await _context.CarImages.Where(x => x.CarId == carId).Select(x => x.ImageUrl).ToListAsync();
            return images;
        }

        public async Task<string> GetPrimaryImage(int carId)
        {
            var image = await _context.CarImages.Where(x => x.CarId == carId).Where(x => x.IsPrimary == true).Select(x => x.ImageUrl).FirstOrDefaultAsync();
            return image;
        }

        public bool Save()
        {
            var result = _context.SaveChanges();
            return result > 0;
        }

        public bool Update(CarImage carImage)
        {
            throw new NotImplementedException();
        }
    }
}
