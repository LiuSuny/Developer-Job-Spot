using DevSpot.Constants;
using Microsoft.AspNetCore.Identity;

namespace DevSpot.Data
{
    public class RoleSeeder
    {
        public static async Task SeedRoleAsync(IServiceProvider serviceProvider)
        {
            var rolemanager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await rolemanager.RoleExistsAsync(Roles.Admin))
            {
                await rolemanager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            if (!await rolemanager.RoleExistsAsync(Roles.JobSeeker))
            {
                await rolemanager.CreateAsync(new IdentityRole(Roles.JobSeeker));
            }

            if (!await rolemanager.RoleExistsAsync(Roles.Employer))
            {
                await rolemanager.CreateAsync(new IdentityRole(Roles.Employer));
            }
        }
    }
}
