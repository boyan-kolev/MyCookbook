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

        public const int IngredientMaxLength = 200;
        public const int IngredientMinLength = 2;
    }
}
