namespace MyCookbook.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task CreateAsync(int recipeId, string userId, string content);

        Task AddReplyToCommentAsync(string commentId, string userId, string content);

        Task EditAsync(string commentId, string content);

        Task EditReplyToCommentAsync(string replyId, string content);

        Task DeleteAsync(string commentId);

        void DeleteReplyFromComment(string replyId);

        bool IsCommentUser(string userId, string commentId);

        bool IsReplyUser(string userId, string replyId);

        Task DeleteCommentsByRecipeIdAsync(int recipeId);
    }
}
