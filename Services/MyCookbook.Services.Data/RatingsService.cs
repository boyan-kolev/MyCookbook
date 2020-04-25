namespace MyCookbook.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyCookbook.Data.Common.Repositories;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;

    public class RatingsService : IRatingsService
    {
        private readonly IRepository<Rating> ratingsRepository;

        public RatingsService(IRepository<Rating> ratingsRepository)
        {
            this.ratingsRepository = ratingsRepository;
        }

        public async Task DeleteRatingsByRecipeIdAsync(int recipeId)
        {
            var ratings = this.ratingsRepository
                .All()
                .Where(r => r.RecipeId == recipeId)
                .ToList();

            foreach (var rating in ratings)
            {
                this.ratingsRepository.Delete(rating);
            }

            await this.ratingsRepository.SaveChangesAsync();
        }

        public double GetRatings(int recipeId)
        {
            var ratings = this.ratingsRepository
                .All()
                .Where(x => x.RecipeId == recipeId)
                .Average(x => x.Stars);

            return ratings;
        }

        public async Task RatingAsync(int recipeId, string userId, int stars)
        {
            var rating = this.ratingsRepository.All()
                .FirstOrDefault(x => x.RecipeId == recipeId && x.UserId == userId);

            if (rating != null)
            {
                rating.Stars = stars;
            }
            else
            {
                rating = new Rating
                {
                    RecipeId = recipeId,
                    UserId = userId,
                    Stars = stars,
                };

                await this.ratingsRepository.AddAsync(rating);
            }

            await this.ratingsRepository.SaveChangesAsync();
        }
    }
}
