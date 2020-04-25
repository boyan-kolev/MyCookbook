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
        private readonly IDeletableEntityRepository<Reply> repliesRepository;
        private readonly IRepliesService repliesService;

        public CommentsService(
            IDeletableEntityRepository<Comment> commentsRepository,
            IDeletableEntityRepository<Reply> repliesRepository,
            IRepliesService repliesService)
        {
            this.commentsRepository = commentsRepository;
            this.repliesRepository = repliesRepository;
            this.repliesService = repliesService;
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

        public async Task EditAsync(string commentId, string content)
        {
            var comment = this.commentsRepository
                .All()
                .FirstOrDefault(c => c.Id == commentId);

            comment.Content = content;

            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task EditReplyToCommentAsync(string replyId, string content)
        {
            var reply = this.repliesRepository
                .All()
                .FirstOrDefault(r => r.Id == replyId);

            reply.Content = content;
            await this.repliesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string commentId)
        {
            var comment = await this.commentsRepository.GetByIdWithDeletedAsync(commentId);

            this.commentsRepository.Delete(comment);
            var repliesInComments = this.repliesRepository
                .All()
                .Where(r => r.CommentId == comment.Id)
                .ToList();

            foreach (var reply in repliesInComments)
            {
                this.repliesRepository.Delete(reply);
            }

            await this.commentsRepository.SaveChangesAsync();
            await this.repliesRepository.SaveChangesAsync();
        }

        public void DeleteReplyFromComment(string replyId)
        {
            var reply = this.repliesRepository
                .All()
                .FirstOrDefault(r => r.Id == replyId);

            this.repliesRepository.Delete(reply);
            this.repliesRepository.SaveChangesAsync();
            this.commentsRepository.SaveChangesAsync();
        }

        public bool IsCommentUser(string userId, string commentId)
        {
            var commentUserId = this.commentsRepository
                .All()
                .Where(c => c.Id == commentId)
                .Select(c => c.UserId)
                .FirstOrDefault();

            return commentUserId == userId;
        }

        public bool IsReplyUser(string userId, string replyId)
        {
            var replyUserId = this.repliesRepository
                .All()
                .Where(r => r.Id == replyId)
                .Select(r => r.UserId)
                .FirstOrDefault();

            return replyUserId == userId;
        }

        public async Task DeleteCommentsByRecipeIdAsync(int recipeId)
        {
            var comments = this.commentsRepository
                .All()
                .Where(c => c.RecipeId == recipeId)
                .ToList();

            foreach (var comment in comments)
            {
                this.commentsRepository.Delete(comment);
                await this.repliesService.DeleteAllByCommentsIdAsync(comment.Id);
            }

            await this.commentsRepository.SaveChangesAsync();
        }
    }
}
