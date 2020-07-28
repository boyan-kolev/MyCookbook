namespace MyCookbook.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Administration.Cuisines.Create;
    using MyCookbook.Web.ViewModels.Administration.Cuisines.Delete;
    using MyCookbook.Web.ViewModels.Administration.Cuisines.Edit;
    using MyCookbook.Web.ViewModels.Administration.Cuisines.Manage;

    public class CuisinesController : AdministrationController
    {
        private const string IsExistCuisineError = "Съществува национална кухня с това име, моля изберете друго име!";
        private const string HasRecipesInCuisineError = "Има рецепти в тази национална кухня. Не може да бъде изтрита!";
        private readonly ICuisinesService cuisinesService;

        public CuisinesController(ICuisinesService cuisinesService)
        {
            this.cuisinesService = cuisinesService;
        }

        public IActionResult Manage()
        {
            var cuisines = this.cuisinesService.GetAll<AdminCuisinesAllViewModel>();
            var viewModel = new AdminCuisinesManageViewModel { Cuisines = cuisines };

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CuisinesCreateInputModel input)
        {
            var isExistCuisineName = this.cuisinesService.IsExist(input.Name);

            if (isExistCuisineName)
            {
                this.ViewData["Error"] = IsExistCuisineError;
            }

            if (!this.ModelState.IsValid || isExistCuisineName)
            {
                return this.View();
            }

            await this.cuisinesService.CreateAsync(input.Name, input.ImageUrl);

            return this.Redirect("/Administration/Cuisines/Manage");
        }

        public IActionResult Edit(int id)
        {
            var viewModel = this.cuisinesService.GetById<CuisinesEditInputModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CuisinesEditInputModel input)
        {
            var isExist = this.cuisinesService.IsExist(input.Name);
            var title = this.cuisinesService.GetNameById(input.Id);
            var isValidTitle = true;

            if (isExist && title.ToLower() != input.Name.ToLower())
            {
                isValidTitle = false;
                this.ViewData["Error"] += IsExistCuisineError;
            }

            if (!this.ModelState.IsValid || !isValidTitle)
            {
                return this.View(input);
            }

            await this.cuisinesService.EditAsync(input.Id, input.Name, input.NewImageUrl);

            return this.Redirect("/Administration/Cuisines/Manage");
        }

        public async Task<IActionResult> Delete(int cuisineId)
        {
            var cuisine = this.cuisinesService.GetById<CuisinesDeleteViewModel>(cuisineId);

            if (cuisine.CountOfRecipes <= 0)
            {
                await this.cuisinesService.DeleteAsync(cuisineId);
            }
            else
            {
                this.TempData["Error"] += HasRecipesInCuisineError;
            }

            return this.Redirect("/Administration/Cuisines/Manage");
        }
    }
}
