namespace MyCookbook.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class AttributesConstraints
    {
        // Recipe
        public const int RecipeTitleMinLength = 3;
        public const int RecipeTitleMaxLength = 80;

        public const int RecipeAdviceMinLength = 8;
        public const int RecipeAdviceMaxLength = 800;

        public const int RecipeServingsMinValue = 0;
        public const int RecipeServingsMaxValue = 200;

        public const int RecipePrepTimeMinValue = 0;
        public const int RecipePrepTimeMaxValue = 300;

        public const int RecipeCookTimeMinValue = 0;
        public const int RecipeCookTimeMaxValue = 500;

        public const int RecipeImagesMaxCount = 5;
        public const int RecipeImageMaxSize = 5 * 1024 * 1024;

        public const int IngredientNameMaxLength = 80;
        public const int IngredientNameMinLength = 2;
        public const int IngredientsNamesMaxLength = 800;
        public const int IngredientsNamesMinLength = 25;

        public const int RecipeDescriptionMaxLength = 2000;
        public const int RecipeDescriptionMinLength = 50;

        // Users
        public const int UserPasswordMaxLength = 100;
        public const int UserPasswordMinLength = 6;

        public const int UserUsernameMaxLength = 256;
        public const int UserUsernameMinLength = 3;

        public const int UserFirstNameMaxLength = 20;
        public const int UserFirstNameMinLength = 3;

        public const int UserLastNameMaxLength = 20;
        public const int UserLastNameMinLength = 3;
    }
}
