namespace MyCookbook.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using MyCookbook.Data.Models;

    public interface IImagesService
    {
        Task AddAsync(Image image);

        Task DeleteImagesByRecipeIdAsync(int recipeId);
    }
}
