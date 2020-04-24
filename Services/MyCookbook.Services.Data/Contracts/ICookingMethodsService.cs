﻿namespace MyCookbook.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using MyCookbook.Data.Models;

    public interface ICookingMethodsService
    {
        T[] GetAll<T>();

        string GetNameById(int cookingMethodId);
    }
}
