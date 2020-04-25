namespace MyCookbook.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyCookbook.Data.Common.Repositories;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;

    public class ImagesService : IImagesService
    {
        private readonly IDeletableEntityRepository<Image> imageRepository;

        public ImagesService(IDeletableEntityRepository<Image> imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        public async Task AddAsync(Image image)
        {
            await this.imageRepository.AddAsync(image);
        }

        public async Task DeleteImagesByRecipeIdAsync(int recipeId)
        {
            var images = this.imageRepository
                .All()
                .Where(img => img.RecipeId == recipeId)
                .ToList();


            foreach (var image in images)
            {
                this.imageRepository.Delete(image);
            }

            await this.imageRepository.SaveChangesAsync();
        }
    }
}
