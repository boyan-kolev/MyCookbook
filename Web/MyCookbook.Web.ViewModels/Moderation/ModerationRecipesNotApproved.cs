namespace MyCookbook.Web.ViewModels.Moderation
{
    using System;
    using System.Linq;

    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class ModerationRecipesNotApproved : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string TitlePictureUrl { get; set; }

        public string AuthorFullName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, ModerationRecipesNotApproved>()
                .ForMember(
                dest => dest.AuthorFullName,
                opt => opt.MapFrom(x => $"{x.Author.FirstName} {x.Author.LastName}"))
                .ForMember(
                dest => dest.TitlePictureUrl,
                opt => opt.MapFrom(x => x.Images.Where(img => img.IsTitlePhoto).Select(img => img.Url).FirstOrDefault()));
        }
    }
}
