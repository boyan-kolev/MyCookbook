namespace MyCookbook.Web.ViewModels.Recipes.Details.ServiceModels
{
    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsCookingMethodsServiceModel : IMapFrom<RecipeCookingMethod>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<RecipeCookingMethod, RecipeDetailsCookingMethodsServiceModel>()
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(x => x.CookingMethod.Name));
        }
    }
}
