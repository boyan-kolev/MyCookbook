namespace MyCookbook.Web.ViewModels.Recipes.Details
{
    using System;

    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Data.Models.Enums;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsReplyUserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string FullName { get; set; }

        public DateTime Birthdate { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public string ProfilePhoto { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, RecipeDetailsReplyUserViewModel>()
                .ForMember(
                opt => opt.FullName,
                dest => dest.MapFrom(x => $"{x.FirstName} {x.LastName}"));
        }
    }
}
