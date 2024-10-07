using CarSalesWebsite.Interfaces;
using CarSalesWebsite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CarSalesWebsite.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
  

        [HttpGet]
        public async Task<IActionResult> Profile(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            var userProfile = new UserDetailVM()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreatedDate = user.CreatedDate,
                ProfileImageUrl = user.ProfileImageUrl,
                Cars = user.Cars
            };
            return View(userProfile);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfileImage(UserDetailVM userDetail)
        {
            if(userDetail.ProfileImage != null && userDetail.ProfileImage.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(userDetail.ProfileImage.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("ProfileImage", "Only image files (jpg, jpeg, png) are allowed.");
                    return RedirectToAction("Profile", new { id = userDetail.Id });
                }

                if (System.IO.File.Exists(userDetail.ProfileImageUrl))
                { System.IO.File.Delete(userDetail.ProfileImageUrl); }

                var filePath = Path.Combine("wwwroot/images/users", userDetail.ProfileImage.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await userDetail.ProfileImage.CopyToAsync(stream);
                }

                var user = await _userRepository.GetUserByIdAsync(userDetail.Id);
                user.ProfileImageUrl = $"/images/users/{userDetail.ProfileImage.FileName}";
                _userRepository.Update(user);
            }

            return RedirectToAction("Profile", new { id = userDetail.Id });
        }
    }
}
