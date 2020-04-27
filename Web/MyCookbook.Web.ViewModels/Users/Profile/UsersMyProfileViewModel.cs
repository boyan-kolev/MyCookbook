namespace MyCookbook.Web.ViewModels.Users.Profile
{
    using System;

    using AutoMapper;
    using MyCookbook.Common;
    using MyCookbook.Data.Models;
    using MyCookbook.Data.Models.Enums;
    using MyCookbook.Services.Mapping;

    public class UsersMyProfileViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime Birthdate { get; set; }

        public string ProfilePhoto { get; set; }

        public int RecipesCount { get; set; }

        public int CookedRecipesCount { get; set; }

        public int FavoriteRecipesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UsersMyProfileViewModel>()
                .ForMember(
                dest => dest.RecipesCount,
                opt => opt.MapFrom(x => x.Recipes.Count))
                .ForMember(
                dest => dest.CookedRecipesCount,
                opt => opt.MapFrom(x => x.CookedRecipes.Count))
                .ForMember(
                dest => dest.FavoriteRecipesCount,
                opt => opt.MapFrom(x => x.FavoriteRecipes.Count))
                .ForMember(
                dest => dest.ProfilePhoto,
                opt => opt.MapFrom(
                    x => x.ProfilePhoto == null && x.Gender == Gender.Male
                    ? GlobalConstants.DefaultUserPhotoMaleUrl
                    : x.ProfilePhoto == null && x.Gender == Gender.Female
                    ? GlobalConstants.DefaultUserPhotoFemaleUrl
                    : x.ProfilePhoto));
        }
    }
}
