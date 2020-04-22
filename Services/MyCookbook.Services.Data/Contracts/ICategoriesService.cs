namespace MyCookbook.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        string GetNameById(int categoryId);

        T GetByName<T>(string name);
    }
}
