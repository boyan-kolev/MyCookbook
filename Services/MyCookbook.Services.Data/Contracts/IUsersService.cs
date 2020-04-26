namespace MyCookbook.Services.Data.Contracts
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IUsersService
    {
        int GetAge(DateTime birthdate);

        bool IsUserRecipeAuthor(string userId, int recipeId);

        T GetById<T>(string userId);

        string GetProfilePictureUrl(string userId);

        Task ChangeProfilePictureAsync(string userId, IFormFile newPicture);
    }
}
