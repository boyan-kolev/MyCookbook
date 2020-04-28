namespace MyCookbook.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyCookbook.Data.Common.Repositories;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Contracts;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Services.Mapping;

    public class CookingMethodsService : ICookingMethodsService
    {
        private const string CloudinaryFolderName = "Начини на приготвяне";
        private readonly IDeletableEntityRepository<CookingMethod> cookingMethodsRepository;
        private readonly ICloudinaryService cloudinaryService;

        public CookingMethodsService(
            IDeletableEntityRepository<CookingMethod> cookingMethodsRepository,
            ICloudinaryService cloudinaryService)
        {
            this.cookingMethodsRepository = cookingMethodsRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task CreateAsync(string name, IFormFile image)
        {
            var imageUrl = await this.cloudinaryService
                .UploadAsync(image, image.FileName, CloudinaryFolderName);
            var cookingMethod = new CookingMethod
            {
                Name = name,
                ImageUrl = imageUrl,
            };

            await this.cookingMethodsRepository.AddAsync(cookingMethod);
            await this.cookingMethodsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int cookingMethodId)
        {
            var cookingMethod = this.cookingMethodsRepository
                .All()
                .Where(c => c.Id == cookingMethodId)
                .FirstOrDefault();

            this.cookingMethodsRepository.Delete(cookingMethod);
            await this.cookingMethodsRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string name, IFormFile image)
        {
            var cookingMethod = this.cookingMethodsRepository
                .All()
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (image != null)
            {
                var imageUrl = await this.cloudinaryService
                    .UploadAsync(image, image.FileName, CloudinaryFolderName);

                cookingMethod.ImageUrl = imageUrl;
            }

            cookingMethod.Name = name;

            this.cookingMethodsRepository.Update(cookingMethod);
            await this.cookingMethodsRepository.SaveChangesAsync();
        }

        public T[] GetAll<T>()
        {
            var cookingMethods = this.cookingMethodsRepository.All()
                .OrderBy(x => x.Name)
                .To<T>()
                .ToArray();

            return cookingMethods;
        }

        public T GetById<T>(int id)
        {
            var cookingMethod = this.cookingMethodsRepository
                .All()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();

            return cookingMethod;
        }

        public string GetNameById(int cookingMethodId)
        {
            var cookingMethodName = this.cookingMethodsRepository
                .All()
                .Where(c => c.Id == cookingMethodId)
                .Select(c => c.Name)
                .FirstOrDefault();

            return cookingMethodName;
        }

        public bool IsExist(string name)
        {
            var result = this.cookingMethodsRepository
                .All()
                .Any(c => c.Name.ToLower() == name.ToLower());

            return result;
        }
    }
}
