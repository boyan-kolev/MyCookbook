namespace MyCookbook.Services.Data.Contracts
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyCookbook.Web.ViewModels.Users.Profile;

    public interface IUsersService
    {
        int GetAge(DateTime birthdate);

        bool IsUserRecipeAuthor(string userId, int recipeId);

        T GetById<T>(string userId);

        string GetProfilePictureUrl(string userId);

        Task ChangeProfilePictureAsync(string userId, IFormFile newPicture);

        FirstAndLastNameDto GetFirstAndLastName(string userId);

        Task ChangeFirstAndLastNameAsync(string userId, string firstName, string lastName);

        DateTime GetBirthdate(string userId);

        Task ChangeBirthdate(string userId, DateTime birthdate);
    }
}
