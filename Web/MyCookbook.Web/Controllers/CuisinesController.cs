namespace MyCookbook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Common;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Cuisines.All;
    using MyCookbook.Web.ViewModels.Cuisines.ByName;
    using MyCookbook.Web.ViewModels.Partials;
    using System;

    public class CuisinesController : BaseController
    {
        private readonly ICuisinesService cuisinesService;
        private readonly IRecipesService recipesService;

        public CuisinesController(
            ICuisinesService cuisinesService,
            IRecipesService recipesService)
        {
            this.cuisinesService = cuisinesService;
            this.recipesService = recipesService;
        }

        public IActionResult All()
        {
            var cuisines = this.cuisinesService.GetAll<CuisinesAllViewModel>();
            var viewModel = new CuisineViewModel { Cuisines = cuisines };

            return this.View(viewModel);
        }

        public IActionResult ByName(string name, int page = 1)
        {
            var viewModel = this.cuisinesService.GetByName<CuisineByNameViewModel>(name);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.ApprovedRecipes = this.recipesService.GetByCuisineId<ListRecipesCollectionPartailViewModel>(viewModel.Id, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);

            var count = this.recipesService.GetCountByCuisineId(viewModel.Id);
            viewModel.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPage);
            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
