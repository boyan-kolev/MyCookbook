namespace MyCookbook.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Comments;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(
            ICommentsService commentsService,
            UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CommentCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Details", "Recipes", new { id = input.RecipeId });
            }

            var userId = this.userManager.GetUserId(this.User);

            if (input.CommentId == null)
            {
                await this.commentsService.CreateAsync(input.RecipeId, userId, input.Content);
            }
            else
            {
                await this.commentsService.AddReplyToCommentAsync(input.CommentId, userId, input.Content);
            }

            return this.RedirectToAction("Details", "Recipes", new { id = input.RecipeId });
        }
    }
}
