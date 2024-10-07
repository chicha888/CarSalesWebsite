using CarSalesWebsite.Models;

namespace CarSalesWebsite.Interfaces
{
    public interface ICarImageRepository
    {
        public Task<List<string>> GetCarImagesByIdAsync(int carId);
        public Task<List<CarImage>> GetImageFullInfoByIdAsync(int carId);
        public Task<string> GetPrimaryImage(int carId);
        public bool Add(CarImage carImage);
        public bool Update(CarImage carImage);
        public bool Delete(CarImage carImage);
        public bool Save();
    }
}
