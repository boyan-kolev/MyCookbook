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

    public class ModeratorsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (userManager.Users.Any(u => u.Email == configuration["Moderator:Email"]))
            {
                return;
            }

            var user = new ApplicationUser
            {
                Email = configuration["Moderator:Email"],
                UserName = configuration["Moderator:UserName"],
                FirstName = configuration["Moderator:FirstName"],
                LastName = configuration["Moderator:LastName"],
                ProfilePhoto = configuration["Moderator:ProfilePhoto"],
                Birthdate = DateTime.Parse(configuration["Moderator:Birthdate"]),
                Gender = Gender.Male,
            };

            var rootPassword = configuration["Moderator:Password"];

            var result = await userManager.CreateAsync(user, rootPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, GlobalConstants.ModeratorRoleName);
            }
        }
    }
}
