﻿namespace MyCookbook.Web.ViewModels.ViewComponents
{
    using System;
    using System.Linq;

    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class LastCreatedRecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TitlePhotoUrl { get; set; }

        public double Rating { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, LastCreatedRecipeViewModel>()
                .ForMember(
                    dest => dest.TitlePhotoUrl,
                    opt => opt
                    .MapFrom(x => x.Images.Where(x => x.IsTitlePhoto).Select(x => x.Url).FirstOrDefault()))
                .ForMember(
                    dest => dest.Title,
                    opt => opt
                    .MapFrom(x => x.Title.Length < 40 ? x.Title : x.Title.Substring(0, 40) + "..."))
                .ForMember(
                    dest => dest.Rating,
                    opt => opt.MapFrom(x => x.Ratings.Average(r => r.Stars)));
        }
    }
}
