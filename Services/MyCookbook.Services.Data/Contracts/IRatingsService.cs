namespace MyCookbook.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRatingsService
    {
        Task RatingAsync(int recipeId, string userId, int stars);

        double GetRatings(int recipeId);

        Task DeleteRatingsByRecipeIdAsync(int recipeId);
    }
}
