namespace MyCookbook.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICuisinesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        string GetNameById(int cuisineId);

        T GetById<T>(int cuisineId);

        T GetByName<T>(string name);

        Task CreateAsync(string name, IFormFile image);

        bool IsExist(string name);

        Task EditAsync(int id, string name, IFormFile image);

        Task DeleteAsync(int cuisinesId);
    }
}
