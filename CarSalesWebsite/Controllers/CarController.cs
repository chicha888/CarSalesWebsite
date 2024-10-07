using CarSalesWebsite.Interfaces;
using CarSalesWebsite.Models;
using CarSalesWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace CarSalesWebsite.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarImageRepository _carImageRepository;

        public CarController(ICarRepository carRepository, ICarImageRepository carImageRepository)
        {
            _carRepository = carRepository;
            _carImageRepository = carImageRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Info(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            var images = await _carImageRepository.GetCarImagesByIdAsync(id);

            var carInfo = new CarInfoVM()
            {
                User = car.User,
                Make = car.Make,
                Model = car.Model,
                Mileage = car.Mileage,
                Price = car.Price,
                Color = car.Color,
                Transmission = car.Transmission,
                BodyType = car.BodyType,
                FuelType = car.FuelType,
                Description = car.Description,
                EngineCapacity = car.EngineCapacity,
                Location = car.Location,
                DateAdded = car.DateAdded,
                Year = car.Year,
                IsSold = car.IsSold,
                CarImages = images,
            };

            return View(carInfo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (car.UserId != userId)
            {
                return RedirectToAction("Error", "Home");
            }

            var carVM = new EditCarVM()
            {
                CarId = id,
                UserId = car.UserId,
                Make = car.Make,
                Model = car.Model,
                Mileage = car.Mileage,
                Price = car.Price,
                Color = car.Color,
                Transmission = car.Transmission,
                BodyType = car.BodyType,
                FuelType = car.FuelType,
                Description = car.Description,
                EngineCapacity = car.EngineCapacity,
                Location = car.Location,
                IsSold = car.IsSold,
                Year = car.Year,
            };

            return View(carVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCarVM carVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit car");
                return View("Edit", carVM);
            }

            if(carVM.CarImages != null && carVM.CarImages.Count > 0)
            {
                var oldImages = await _carImageRepository.GetImageFullInfoByIdAsync(carVM.CarId);
                foreach(var image in oldImages)
                {
                    if (System.IO.File.Exists(image.ImageUrl))
                    { System.IO.File.Delete(image.ImageUrl); }

                    _carImageRepository.Delete(image);
                }

                bool firstImage = true;
                foreach (var image in carVM.CarImages)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("CarImages", "Only image files (jpg, jpeg, png) are allowed.");
                        return View(carVM);
                    }

                    if (image.Length > 0)
                    {
                        var filePath = Path.Combine("wwwroot/images/cars", image.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        var carImage = new CarImage
                        {
                            CarId = carVM.CarId,
                            ImageUrl = $"/images/cars/{image.FileName}",
                            DateUploaded = DateTime.Now,
                            IsPrimary = firstImage
                        };

                        firstImage = false;

                        _carImageRepository.Add(carImage);
                    }
                }
            }

            var car = new Car()
            {
                Id = carVM.CarId,
                UserId = carVM.UserId,
                Make = carVM.Make,
                Model = carVM.Model,
                Mileage = carVM.Mileage,
                Price = carVM.Price,
                Color = carVM.Color,
                Transmission = carVM.Transmission,
                BodyType = carVM.BodyType,
                FuelType = carVM.FuelType,
                Description = carVM.Description,
                EngineCapacity = carVM.EngineCapacity,
                Location = carVM.Location,
                IsSold = carVM.IsSold,
                Year = carVM.Year,
                DateAdded = DateTime.Now,
            };

            _carRepository.Update(car);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
             _carRepository.Delete(car);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult AddCar()
        {
            var responce = new CreateNewCarVM();
            return View(responce);
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(CreateNewCarVM createNewCarVM)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var newCar = new Car()
                {
                    UserId = userId,
                    Make = createNewCarVM.Make,
                    Model = createNewCarVM.Model,
                    Year = createNewCarVM.Year,
                    Mileage = createNewCarVM.Mileage,
                    Price = createNewCarVM.Price,
                    Transmission = createNewCarVM.Transmission,
                    FuelType = createNewCarVM.FuelType,
                    BodyType = createNewCarVM.BodyType,
                    Color = createNewCarVM.Color,
                    EngineCapacity = createNewCarVM.EngineCapacity,
                    Description = createNewCarVM.Description,
                    IsSold = false,
                    DateAdded = DateTime.Now,
                    Location = createNewCarVM.Location
                };

                _carRepository.Add(newCar);

                var carId = newCar.Id;

                if (createNewCarVM.CarImages != null && createNewCarVM.CarImages.Count > 0)
                {
                    bool firstImage = true;
                    foreach (var image in createNewCarVM.CarImages)
                    {
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                        var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
                        if (!allowedExtensions.Contains(extension))
                        {
                            ModelState.AddModelError("CarImages", "Only image files (jpg, jpeg, png) are allowed.");
                            return View(createNewCarVM);
                        }

                        if (image.Length > 0)
                        {
                            var filePath = Path.Combine("wwwroot/images/cars", image.FileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }

                            var carImage = new CarImage
                            {
                                CarId = carId,
                                ImageUrl = $"/images/cars/{image.FileName}",
                                DateUploaded = DateTime.Now,
                                IsPrimary = firstImage
                            };

                            firstImage = false;

                            _carImageRepository.Add(carImage);
                        }
                    }
                }

                return RedirectToAction("Index", "Home");
            }

            return View(createNewCarVM);
        }
    }
}
