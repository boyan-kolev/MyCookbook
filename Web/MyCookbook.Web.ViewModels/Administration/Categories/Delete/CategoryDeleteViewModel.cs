namespace MyCookbook.Web.ViewModels.Administration.Categories.Delete
{
    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class CategoryDeleteViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int CountOfRecipes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Category, CategoryDeleteViewModel>()
                .ForMember(
                    dest => dest.CountOfRecipes,
                    opt => opt
                    .MapFrom(x => x.Recipes.Count));
        }
    }
}
