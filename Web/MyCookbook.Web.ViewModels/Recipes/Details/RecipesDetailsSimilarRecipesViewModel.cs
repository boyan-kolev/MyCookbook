namespace MyCookbook.Web.ViewModels.Recipes.Details
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipesDetailsSimilarRecipesViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TitlePhotoUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipesDetailsSimilarRecipesViewModel>()
                .ForMember(
                dest => dest.TitlePhotoUrl,
                opt => opt.MapFrom(x => x.Images.Where(x => x.IsTitlePhoto).Select(x => x.Url).FirstOrDefault()));
        }
    }
}
