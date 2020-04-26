namespace MyCookbook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyCookbook.Data.Common.Repositories;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Contracts;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Services.Mapping;

    public class UsersService : IUsersService
    {
        private const string CloudinaryFolderName = "Профилни снимки";
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly ICloudinaryService cloudinaryService;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            ICloudinaryService cloudinaryService)
        {
            this.userRepository = userRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task ChangeProfilePictureAsync(string userId, IFormFile newPicture)
        {
            var newProfilePictureUrl = await this.cloudinaryService
                .UploadAsync(newPicture, newPicture.FileName, CloudinaryFolderName);

            var user = this.userRepository
                .All()
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            user.ProfilePhoto = newProfilePictureUrl;
            await this.userRepository.SaveChangesAsync();
        }

        public int GetAge(DateTime birthdate)
        {
            int age = 0;
            age = DateTime.Now.Year - birthdate.Year;
            if (DateTime.Now.DayOfYear < birthdate.DayOfYear)
            {
                age = age - 1;
            }

            return age;
        }

        public T GetById<T>(string userId)
        {
            var userRecipes = this.userRepository
                .All()
                .Where(u => u.Id == userId)
                .To<T>()
                .FirstOrDefault();

            return userRecipes;
        }

        public string GetProfilePictureUrl(string userId)
        {
            var profilePhotoUrl = this.userRepository
                .All()
                .Where(u => u.Id == userId)
                .Select(u => u.ProfilePhoto)
                .FirstOrDefault();

            return profilePhotoUrl;
        }

        public bool IsUserRecipeAuthor(string userId, int recipeId)
        {
            var result = this.userRepository
                .All()
                .Where(u => u.Id == userId)
                .Select(u => u.Recipes.Any(r => r.Id == recipeId))
                .FirstOrDefault();

            return result;
        }
    }
}
