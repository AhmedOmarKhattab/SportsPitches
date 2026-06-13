

using Microsoft.AspNetCore.Identity;
using FiveStadium.Models;
using System.Runtime.CompilerServices;

namespace FiveStadium.Extensions
{
    public static class AppUsersAndRolesSeeding
    {
        public static async Task<WebApplication> SeedUsersAndRoles(this WebApplication application, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var role = new IdentityRole()
                {
                    Name = "Admin"
                };
               
                await roleManager.CreateAsync(role);

            }


            if (!userManager.Users.Any())
            {

             
                var user = new ApplicationUser()
                {
                    UserName = "project",
                    FirstName = "Project",
                    LastName = "pitch",
                    Email = "project@gmail.com",
                    PhoneNumber = "0123456789"
                };


                var result = await userManager.CreateAsync(user, "0123456789");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    //var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    //await userManager.ConfirmEmailAsync(user, token);
                }
               



            }
            return application;
        }
    }
}
