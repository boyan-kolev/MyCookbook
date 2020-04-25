﻿namespace MyCookbook.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IUsersService
    {
        int GetAge(DateTime birthdate);

        bool IsUserRecipeAuthor(string userId, int recipeId);

        T GetById<T>(string userId);
    }
}
