namespace MyCookbook.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MyCookbook.Common;
    using MyCookbook.Data.Models;
    using MyCookbook.Data.Models.Enums;

    public class AdministratorsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (userManager.Users.Any(u => u.Email == configuration["Administrator:Email"]))
            {
                return;
            }

            var user = new ApplicationUser
            {
                Email = configuration["Administrator:Email"],
                UserName = configuration["Administrator:UserName"],
                FirstName = configuration["Administrator:FirstName"],
                LastName = configuration["Administrator:LastName"],
                ProfilePhoto = configuration["Administrator:ProfilePhoto"],
                Birthdate = DateTime.Parse(configuration["Administrator:Birthdate"]),
                Gender = Gender.Male,
            };

            var rootPassword = configuration["Administrator:Password"];

            var result = await userManager.CreateAsync(user, rootPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
            }
        }
    }
}