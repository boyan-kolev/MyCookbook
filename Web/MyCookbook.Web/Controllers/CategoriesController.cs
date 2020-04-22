namespace MyCookbook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Categories.All;
    using MyCookbook.Web.ViewModels.Categories.ByName;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult All()
        {
            var categories = this.categoriesService.GetAll<CategoriesAllViewModel>();
            var viewModel = new CategoryViewModel { Categories = categories };

            return this.View(viewModel);
        }

        public IActionResult ByName(string name)
        {
            var viewModel = this.categoriesService.GetByName<CategoryByNameViewModel>(name);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
