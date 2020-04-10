namespace MyCookbook.Web.ViewModels.Recipes.Details
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsCookingMethodsViewModel : IMapFrom<RecipeCookingMethod>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<RecipeCookingMethod, RecipeDetailsCookingMethodsViewModel>()
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(x => x.CookingMethod.Name));
        }
    }
}
