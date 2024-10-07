using CarSalesWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace CarSalesWebsite.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllCars();
        Task<Car> GetByIdAsync(int id);
        bool Add(Car car);
        bool Update(Car car);
        bool Delete(Car car);
        bool Save();
    }
}
