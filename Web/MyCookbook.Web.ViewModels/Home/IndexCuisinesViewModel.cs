namespace MyCookbook.Web.ViewModels.Home
{
    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;
    using System.Linq;

    public class IndexCuisinesViewModel : IMapFrom<Cuisine>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int RecipesCount { get; set; }

        public string Url => $"/cuisine/{this.Name.Replace(' ', '-')}";

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Cuisine, IndexCuisinesViewModel>()
                .ForMember(
                opt => opt.RecipesCount,
                dest => dest.MapFrom(x => x.Recipes.Where(r => r.IsApproved == true).Count()));
        }
    }
}
