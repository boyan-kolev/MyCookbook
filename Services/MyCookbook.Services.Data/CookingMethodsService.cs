namespace MyCookbook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyCookbook.Data.Common.Repositories;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Services.Mapping;

    public class CookingMethodsService : ICookingMethodsService
    {
        private readonly IDeletableEntityRepository<CookingMethod> cookingMethodsRepository;

        public CookingMethodsService(IDeletableEntityRepository<CookingMethod> cookingMethodsRepository)
        {
            this.cookingMethodsRepository = cookingMethodsRepository;
        }

        public T[] GetAll<T>()
        {
            var cookingMethods = this.cookingMethodsRepository.All()
                .OrderBy(x => x.Name)
                .To<T>()
                .ToArray();

            return cookingMethods;
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
    }
}
