using DevSpot.Constants;
using Microsoft.AspNetCore.Identity;

namespace DevSpot.Data
{
    public class UserSeeder
    {
        public static async Task SeedUserAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            await CreatUserWithRoleAsync(userManager, "admin@divspot.com", "Admin123!", Roles.Admin);
            await CreatUserWithRoleAsync(userManager, "jobseeker@divspot.com", "JobSeeker123!", Roles.JobSeeker);
            await CreatUserWithRoleAsync(userManager, "employer@divspot.com", "Emloyer123!", Roles.Employer);

            //if (await userManager.FindByEmailAsync("admin@divspot.com") == null)
            //{
            //    var user = new IdentityUser
            //    {
            //        Email = "admin@divspot.com",
            //        EmailConfirmed = true,
            //        UserName = "admin@divspot.com"
            //    };
            //    var result = await userManager.CreateAsync(user, "Admin123!");
            //    if (result.Succeeded)
            //    {
            //        await userManager.AddToRoleAsync(user, Roles.Admin);
            //    }
            //    else
            //    {
            //        throw new Exception($"Failed Creating user with email{user.Email}. Error: {string.Join(",", result.Errors)}");
            //    }

            // }
        }

        private static async Task CreatUserWithRoleAsync(
            UserManager<IdentityUser> userManager, string email,
            string password, string role)
        {         

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser
                {
                    Email =email,
                    EmailConfirmed = true,
                    UserName = email
                };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user,role);
                }
                else
                {
                    throw new Exception($"Failed Creating user with email{user.Email}. Error: {string.Join(",", result.Errors)}");
                }

            }
        }
    }
}
