namespace MyCookbook.Web.ViewModels.Administration.Cuisines.Delete
{
    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class CuisinesDeleteViewModel : IMapFrom<Cuisine>, IHaveCustomMappings
    {
        public int CountOfRecipes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Cuisine, CuisinesDeleteViewModel>()
                .ForMember(
                    dest => dest.CountOfRecipes,
                    opt => opt
                    .MapFrom(x => x.Recipes.Count));
        }
    }
}
