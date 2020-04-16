namespace MyCookbook.Web.ViewModels.Recipes.Details.ServiceModels
{
    using System;

    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsAuthorServiceModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string FullName { get; set; }

        public DateTime Birthdate { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string ProfilePhoto { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, RecipeDetailsAuthorServiceModel>()
            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(x => $"{x.FirstName} {x.LastName}"));
        }
    }
}
