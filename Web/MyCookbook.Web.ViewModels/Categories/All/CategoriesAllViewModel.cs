namespace MyCookbook.Web.ViewModels.Categories.All
{
    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;
    using System.Linq;

    public class CategoriesAllViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int RecipesCount { get; set; }

        public string Url => $"/category/{this.Name.Replace(' ', '-')}";

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Category, CategoriesAllViewModel>()
                .ForMember(
                opt => opt.RecipesCount,
                dest => dest.MapFrom(x => x.Recipes.Where(r => r.IsApproved == true).Count()));
        }
    }
}
