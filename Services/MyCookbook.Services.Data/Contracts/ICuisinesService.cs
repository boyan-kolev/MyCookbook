namespace MyCookbook.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ICuisinesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(int cuisineId);
    }
}
