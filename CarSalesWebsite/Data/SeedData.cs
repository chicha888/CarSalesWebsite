using CarSalesWebsite.Models;
using Microsoft.AspNetCore.Identity;

namespace CarSalesWebsite.Data
{
    public class SeedData
    {
        public static async Task AddUserRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "kirilldeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null) 
                {
                    var newAdminUser = new AppUser 
                    { 
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        UserName = adminUserEmail,
                        FirstName = "Kirill",
                        LastName = "Maslo",
                        CreatedDate = DateTime.Now
                    };

                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string userEmail = "testuser@gmail.com";

                var user = await userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    var newUser = new AppUser
                    {
                        Email = userEmail,
                        EmailConfirmed = true,
                        UserName = userEmail,
                        FirstName = "Test",
                        LastName = "User",
                        CreatedDate = DateTime.Now
                    };

                    await userManager.CreateAsync(newUser, "Coding@4321?");
                    await userManager.AddToRoleAsync(newUser, UserRoles.User);
                }
            }
        }
    }
}
