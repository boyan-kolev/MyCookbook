namespace MyCookbook.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Common;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Comments;

    public class CommentsController : BaseController
    {
        private const string ContentLengthError = "Съдържанието на коментара трябва да бъде между 8 и 500 символа!";
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
            var isValidContent = true;

            if (string.IsNullOrEmpty(input.Content) || string.IsNullOrWhiteSpace(input.Content))
            {
                isValidContent = false;
                this.TempData["CommentContentError"] = ContentLengthError;
            }
            else if (input.Content.Length < AttributesConstraints.CommentContentMinLength
                || input.Content.Length > AttributesConstraints.CommentContentMaxLength)
            {
                isValidContent = false;
                this.TempData["CommentContentError"] = ContentLengthError;
            }

            if (!this.ModelState.IsValid || !isValidContent)
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(CommentEditInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var isValidComment = false;

            if (input.ReplyId == null)
            {
                isValidComment = this.commentsService.IsCommentUser(userId, input.CommentId);
            }
            else
            {
                isValidComment = this.commentsService.IsReplyUser(userId, input.ReplyId);
            }

            var isValidContent = true;

            if (string.IsNullOrEmpty(input.Content) || string.IsNullOrWhiteSpace(input.Content))
            {
                isValidContent = false;
                this.TempData["CommentContentError"] = ContentLengthError;
            }
            else if (input.Content.Length < AttributesConstraints.CommentContentMinLength
                || input.Content.Length > AttributesConstraints.CommentContentMaxLength)
            {
                isValidContent = false;
                this.TempData["CommentContentError"] = ContentLengthError;
            }

            if (!this.ModelState.IsValid || !isValidComment || !isValidContent)
            {
                return this.RedirectToAction("Details", "Recipes", new { id = input.RecipeId });
            }

            if (input.ReplyId == null)
            {
                await this.commentsService.EditAsync(input.CommentId, input.Content);
            }
            else
            {
                await this.commentsService.EditReplyToCommentAsync(input.ReplyId, input.Content);
            }

            return this.RedirectToAction("Details", "Recipes", new { id = input.RecipeId });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(CommentDeleteInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var isValidComment = false;

            if (input.ReplyId == null)
            {
                isValidComment = this.commentsService.IsCommentUser(userId, input.CommentId);
            }
            else
            {
                isValidComment = this.commentsService.IsReplyUser(userId, input.ReplyId);
            }

            if (!this.ModelState.IsValid || !isValidComment)
            {
                return this.RedirectToAction("Details", "Recipes", new { id = input.RecipeId });
            }

            if (input.ReplyId == null)
            {
                await this.commentsService.DeleteAsync(input.CommentId);
            }
            else
            {
                this.commentsService.DeleteReplyFromComment(input.ReplyId);
            }

            return this.RedirectToAction("Details", "Recipes", new { id = input.RecipeId });
        }
    }
}
