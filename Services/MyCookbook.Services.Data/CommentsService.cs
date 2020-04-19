namespace MyCookbook.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyCookbook.Data.Common.Repositories;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task AddReplyToCommentAsync(string commentId, string userId, string content)
        {
            var reply = new Reply
            {
                CommentId = commentId,
                UserId = userId,
                Content = content,
            };

            var comment = this.commentsRepository.All().FirstOrDefault(c => c.Id == commentId);
            comment.Replies.Add(reply);
            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(int recipeId, string userId, string content)
        {
            var comment = new Comment
            {
                RecipeId = recipeId,
                UserId = userId,
                Content = content,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }
    }
}
