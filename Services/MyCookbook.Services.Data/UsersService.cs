﻿namespace MyCookbook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MyCookbook.Data.Common.Repositories;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Services.Mapping;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public int GetAge(DateTime birthdate)
        {
            int age = 0;
            age = DateTime.Now.Year - birthdate.Year;
            if (DateTime.Now.DayOfYear < birthdate.DayOfYear)
            {
                age = age - 1;
            }

            return age;
        }

        public T GetById<T>(string userId)
        {
            var userRecipes = this.userRepository
                .All()
                .Where(u => u.Id == userId)
                .To<T>()
                .FirstOrDefault();

            return userRecipes;
        }

        public bool IsUserRecipeAuthor(string userId, int recipeId)
        {
            var result = this.userRepository
                .All()
                .Where(u => u.Id == userId)
                .Select(u => u.Recipes.Any(r => r.Id == recipeId))
                .FirstOrDefault();

            return result;
        }
    }
}
