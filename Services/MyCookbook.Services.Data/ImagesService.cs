namespace MyCookbook.Services.Data
{
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
    }
}
