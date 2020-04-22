namespace MyCookbook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Cuisines.All;
    using MyCookbook.Web.ViewModels.Cuisines.ByName;

    public class CuisinesController : BaseController
    {
        private readonly ICuisinesService cuisinesService;

        public CuisinesController(ICuisinesService cuisinesService)
        {
            this.cuisinesService = cuisinesService;
        }

        public IActionResult All()
        {
            var cuisines = this.cuisinesService.GetAll<CuisinesAllViewModel>();
            var viewModel = new CuisineViewModel { Cuisines = cuisines };

            return this.View(viewModel);
        }

        public IActionResult ByName(string name)
        {
            var viewModel = this.cuisinesService.GetByName<CuisineByNameViewModel>(name);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
