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

    public class CategoriesService : ICategoriesService
    {
        private const string CloudinaryFolderName = "Категории";
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly ICloudinaryService cloudinaryService;

        public CategoriesService(
            IDeletableEntityRepository<Category> categoriesRepository,
            ICloudinaryService cloudinaryService)
        {
            this.categoriesRepository = categoriesRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task CreateAsync(string name, IFormFile image)
        {
            var imageUrl = await this.cloudinaryService
                .UploadAsync(image, image.FileName, CloudinaryFolderName);
            var category = new Category
            {
                Name = name,
                ImageUrl = imageUrl,
            };

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int categoryId)
        {
            var category = this.categoriesRepository
                .All()
                .Where(c => c.Id == categoryId)
                .FirstOrDefault();

            this.categoriesRepository.Delete(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string name, IFormFile image)
        {
            var category = this.categoriesRepository
                .All()
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (image != null)
            {
                var imageUrl = await this.cloudinaryService
                    .UploadAsync(image, image.FileName, CloudinaryFolderName);

                category.ImageUrl = imageUrl;
            }

            category.Name = name;

            this.categoriesRepository.Update(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query = this.categoriesRepository
                .All()
                .OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var category = this.categoriesRepository
                .All()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();

            return category;
        }

        public T GetByName<T>(string name)
        {
            var category = this.categoriesRepository
                .All()
                .Where(x => x.Name.Replace(" ", "-") == name.Replace(" ", "-"))
                .To<T>()
                .FirstOrDefault();

            return category;
        }

        public string GetNameById(int categoryId)
        {
            var categoryName = this.categoriesRepository
                .All()
                .Where(c => c.Id == categoryId)
                .Select(c => c.Name)
                .FirstOrDefault();

            return categoryName;
        }

        public bool IsExist(string name)
        {
            var result = this.categoriesRepository
                .All()
                .Any(c => c.Name.ToLower() == name.ToLower());

            return result;
        }
    }
}
