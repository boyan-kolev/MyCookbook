namespace MyCookbook.Web.ViewModels.Administration.CookingMethods.Delete
{
    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class CookingMethodsDeleteViewModel : IMapFrom<CookingMethod>, IHaveCustomMappings
    {
        public int CountOfRecipes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CookingMethod, CookingMethodsDeleteViewModel>()
                .ForMember(
                    dest => dest.CountOfRecipes,
                    opt => opt
                    .MapFrom(x => x.RecipesCookingMethods.Count));
        }
    }
}
