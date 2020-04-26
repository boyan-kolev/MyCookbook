namespace MyCookbook.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyCookbook.Common;
    using MyCookbook.Data.Common.Repositories;
    using MyCookbook.Data.Models;
    using MyCookbook.Data.Models.Enums;
    using MyCookbook.Services.Contracts;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.Users.Profile;

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

        public async Task ChangeBirthdate(string userId, DateTime birthdate)
        {
            var user = this.userRepository
                .All()
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            user.Birthdate = birthdate;
            await this.userRepository.SaveChangesAsync();
        }

        public async Task ChangeFirstAndLastNameAsync(string userId, string firstName, string lastName)
        {
            var user = this.userRepository
                .All()
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            user.FirstName = firstName;
            user.LastName = lastName;

            await this.userRepository.SaveChangesAsync();
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

        public DateTime GetBirthdate(string userId)
        {
            var birthdate = this.userRepository
                .All()
                .Where(u => u.Id == userId)
                .Select(u => u.Birthdate)
                .FirstOrDefault();

            return birthdate;
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

        public FirstAndLastNameDto GetFirstAndLastName(string userId)
        {
            var firstAndLastName = this.userRepository
                .All()
                .Where(u => u.Id == userId)
                .Select(u => new FirstAndLastNameDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                })
                .FirstOrDefault();

            return firstAndLastName;
        }

        public string GetProfilePictureUrl(string userId)
        {
            var profilePhotoUrl = this.userRepository
                .All()
                .Where(u => u.Id == userId)
                .Select(u => u.ProfilePhoto)
                .FirstOrDefault();

            if (profilePhotoUrl == null)
            {
                var gender = this.userRepository
                .All()
                .Where(u => u.Id == userId)
                .Select(u => u.Gender)
                .FirstOrDefault();

                if (gender == Gender.Male)
                {
                    profilePhotoUrl = GlobalConstants.DefaultUserPhotoMaleUrl;
                }
                else
                {
                    profilePhotoUrl = GlobalConstants.DefaultUserPhotoFemaleUrl;
                }
            }

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
