namespace MyCookbook.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Administration.CookingMethods.Create;
    using MyCookbook.Web.ViewModels.Administration.CookingMethods.Delete;
    using MyCookbook.Web.ViewModels.Administration.CookingMethods.Edit;
    using MyCookbook.Web.ViewModels.Administration.CookingMethods.Manage;

    public class CookingMethodsController : AdministrationController
    {
        private const string IsExistCookingMethodError = "Съществува начин на приготвяне с това име, моля изберете друго име!";
        private const string HasRecipesInCookingMethodError = "Има добавени рецепти в този начин на приготвяне. Не може да бъде изтрит!";
        private readonly ICookingMethodsService cookingMethodService;

        public CookingMethodsController(ICookingMethodsService cookingMethodService)
        {
            this.cookingMethodService = cookingMethodService;
        }

        public IActionResult Manage()
        {
            var cookingMethods = this.cookingMethodService.GetAll<AdminCookingMethodsAllViewModel>();
            var viewModel = new AdminCookingMethodsManageViewModel { CookingMethods = cookingMethods };

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CookingMethodsCreateInputModel input)
        {
            var isExistCookingMethodName = this.cookingMethodService.IsExist(input.Name);

            if (isExistCookingMethodName)
            {
                this.ViewData["Error"] = IsExistCookingMethodError;
            }

            if (!this.ModelState.IsValid || isExistCookingMethodName)
            {
                return this.View();
            }

            await this.cookingMethodService.CreateAsync(input.Name, input.ImageUrl);

            return this.Redirect("/Administration/CookingMethods/Manage");
        }

        public IActionResult Edit(int id)
        {
            var viewModel = this.cookingMethodService.GetById<CookingMethodsEditInputModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CookingMethodsEditInputModel input)
        {
            var isExist = this.cookingMethodService.IsExist(input.Name);
            var title = this.cookingMethodService.GetNameById(input.Id);
            var isValidTitle = true;

            if (isExist && title.ToLower() != input.Name.ToLower())
            {
                isValidTitle = false;
                this.ViewData["Error"] += IsExistCookingMethodError;
            }

            if (!this.ModelState.IsValid || !isValidTitle)
            {
                return this.View(input);
            }

            await this.cookingMethodService.EditAsync(input.Id, input.Name, input.NewImageUrl);

            return this.Redirect("/Administration/CookingMethods/Manage");
        }

        public async Task<IActionResult> Delete(int cookingMethodId)
        {
            var cookingMethod = this.cookingMethodService.GetById<CookingMethodsDeleteViewModel>(cookingMethodId);

            if (cookingMethod.CountOfRecipes <= 0)
            {
                await this.cookingMethodService.DeleteAsync(cookingMethodId);
            }
            else
            {
                this.TempData["Error"] += HasRecipesInCookingMethodError;
            }

            return this.Redirect("/Administration/CookingMethods/Manage");
        }
    }
}
