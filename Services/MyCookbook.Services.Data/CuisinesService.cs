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

    public class CuisinesService : ICuisinesService
    {
        private const string CloudinaryFolderName = "Национлни кухни";
        private readonly IDeletableEntityRepository<Cuisine> cuisinesRepository;
        private readonly ICloudinaryService cloudinaryService;

        public CuisinesService(
            IDeletableEntityRepository<Cuisine> cuisinesRepository,
            ICloudinaryService cloudinaryService)
        {
            this.cuisinesRepository = cuisinesRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task CreateAsync(string name, IFormFile image)
        {
            var imageUrl = await this.cloudinaryService
                .UploadAsync(image, image.FileName, CloudinaryFolderName);
            var cuisine = new Cuisine
            {
                Name = name,
                ImageUrl = imageUrl,
            };

            await this.cuisinesRepository.AddAsync(cuisine);
            await this.cuisinesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int cuisinesId)
        {
            var cuisine = this.cuisinesRepository
                .All()
                .Where(c => c.Id == cuisinesId)
                .FirstOrDefault();

            this.cuisinesRepository.Delete(cuisine);
            await this.cuisinesRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string name, IFormFile image)
        {
            var cuisine = this.cuisinesRepository
                .All()
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (image != null)
            {
                var imageUrl = await this.cloudinaryService
                    .UploadAsync(image, image.FileName, CloudinaryFolderName);

                cuisine.ImageUrl = imageUrl;
            }

            cuisine.Name = name;

            this.cuisinesRepository.Update(cuisine);
            await this.cuisinesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Cuisine> query = this.cuisinesRepository
                .All()
                .OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int cuisineId)
        {
            var cuisineName = this.cuisinesRepository
                .All()
                .Where(c => c.Id == cuisineId)
                .To<T>()
                .FirstOrDefault();

            return cuisineName;
        }

        public T GetByName<T>(string name)
        {
            var cuisine = this.cuisinesRepository
                .All()
                .Where(x => x.Name.Replace(" ", "-") == name.Replace(" ", "-"))
                .To<T>()
                .FirstOrDefault();

            return cuisine;
        }

        public string GetNameById(int cuisineId)
        {
            var cuisineName = this.cuisinesRepository
                .All()
                .Where(c => c.Id == cuisineId)
                .Select(c => c.Name)
                .FirstOrDefault();

            return cuisineName;
        }

        public bool IsExist(string name)
        {
            var result = this.cuisinesRepository
                .All()
                .Any(c => c.Name.ToLower() == name.ToLower());

            return result;
        }
    }
}
