namespace MyCookbook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MyCookbook.Data.Common.Repositories;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Services.Mapping;

    public class CuisinesService : ICuisinesService
    {
        private readonly IDeletableEntityRepository<Cuisine> cuisinesRepository;

        public CuisinesService(IDeletableEntityRepository<Cuisine> cuisinesRepository)
        {
            this.cuisinesRepository = cuisinesRepository;
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
    }
}
