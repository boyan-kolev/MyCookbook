namespace MyCookbook.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyCookbook.Data.Models;

    public interface ICookingMethodsService
    {
        T[] GetAll<T>();

        string GetNameById(int cookingMethodId);

        T GetById<T>(int id);

        Task CreateAsync(string name, IFormFile image);

        bool IsExist(string name);

        Task EditAsync(int id, string name, IFormFile image);

        Task DeleteAsync(int cookingMethodId);
    }
}
