namespace MyCookbook.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ICookingMethodsService
    {
        T[] GetAll<T>();

        string GetNameById(int cookingMethodId);
    }
}
