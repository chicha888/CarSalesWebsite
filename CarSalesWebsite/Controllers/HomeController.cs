using CarSalesWebsite.Interfaces;
using CarSalesWebsite.Models;
using CarSalesWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace CarSalesWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICarRepository _carRepository;
        private readonly ICarImageRepository _carImageRepository;

        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository, ICarRepository carRepository, ICarImageRepository carImageRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _carRepository = carRepository;
            _carImageRepository = carImageRepository;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carRepository.GetAllCars();

            var carInfos = new List<CarInfoVM>();
            foreach (var car in cars)
            {
                var info = new CarInfoVM()
                {
                    Id = car.Id,
                    User = await _userRepository.GetUserByIdAsync(car.UserId),
                    Make = car.Make,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    Price = car.Price,
                    Description = car.Description,
                    Location = car.Location,
                    DateAdded = car.DateAdded,
                    Year = car.Year,
                    IsSold = car.IsSold,
                    PrimaryImage = await _carImageRepository.GetPrimaryImage(car.Id)
                };

                carInfos.Add(info);
            }

            return View(carInfos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
