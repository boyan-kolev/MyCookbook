﻿namespace MyCookbook.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        string GetNameById(int categoryId);

        T GetByName<T>(string name);

        T GetById<T>(int id);

        Task CreateAsync(string name, IFormFile image);

        bool IsExist(string name);

        Task EditAsync(int id, string name, IFormFile image);

        Task DeleteAsync(int categoryId);
    }
}
