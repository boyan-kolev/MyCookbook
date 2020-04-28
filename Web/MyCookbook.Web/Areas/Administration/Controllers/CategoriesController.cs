namespace MyCookbook.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Administration.Categories.Create;
    using MyCookbook.Web.ViewModels.Administration.Categories.Edit;
    using MyCookbook.Web.ViewModels.Administration.Categories.Manage;

    public class CategoriesController : AdministrationController
    {
        private const string IsExistCategoryError = "Съществува категория с това име, моля изберете друго име!";
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Manage()
        {
            var categories = this.categoriesService.GetAll<AdminCategoriesAllViewModel>();
            var viewModel = new AdminCategoryViewModel { Categories = categories };

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoriesCreateInputModel input)
        {
            var isExistCategoryName = this.categoriesService.IsExist(input.Name);

            if (isExistCategoryName)
            {
                this.ViewData["Error"] = IsExistCategoryError;
            }

            if (!this.ModelState.IsValid || isExistCategoryName)
            {
                return this.View();
            }

            await this.categoriesService.CreateAsync(input.Name, input.ImageUrl);

            return this.Redirect("/Administration/Categories/Manage");
        }

        public IActionResult Edit(int id)
        {
            var viewModel = this.categoriesService.GetById<CategoriesEditInputModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoriesEditInputModel input)
        {
            var isExist = this.categoriesService.IsExist(input.Name);
            var title = this.categoriesService.GetNameById(input.Id);
            var isValidTitle = true;

            if (isExist && title.ToLower() != input.Name.ToLower())
            {
                isValidTitle = false;
                this.ViewData["Error"] += IsExistCategoryError;
            }

            if (!this.ModelState.IsValid || !isValidTitle)
            {
                return this.View(input);
            }

            await this.categoriesService.EditAsync(input.Id, input.Name, input.NewImageUrl);

            return this.Redirect("/Administration/Categories/Manage");
        }

        public async Task<IActionResult> Delete(int categoryId)
        {
            await this.categoriesService.DeleteAsync(categoryId);

            return this.Redirect("/Administration/Categories/Manage");
        }
    }
}
