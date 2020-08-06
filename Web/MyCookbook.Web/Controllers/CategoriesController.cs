namespace MyCookbook.Web.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Common;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Categories.All;
    using MyCookbook.Web.ViewModels.Categories.ByName;
    using MyCookbook.Web.ViewModels.Partials;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipesService recipesService;

        public CategoriesController(
            ICategoriesService categoriesService,
            IRecipesService recipesService)
        {
            this.categoriesService = categoriesService;
            this.recipesService = recipesService;
        }

        public IActionResult All()
        {
            var categories = this.categoriesService.GetAll<CategoriesAllViewModel>();
            var viewModel = new CategoryViewModel { Categories = categories };

            return this.View(viewModel);
        }

        public IActionResult ByName(string name, int page = 1)
        {
            var viewModel = this.categoriesService.GetByName<CategoryByNameViewModel>(name);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.ApprovedRecipes = this.recipesService.GetByCategoryId<ListRecipesCollectionPartailViewModel>(viewModel.Id, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);

            var count = this.recipesService.GetCountByCategoryId(viewModel.Id);
            viewModel.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPage);
            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
