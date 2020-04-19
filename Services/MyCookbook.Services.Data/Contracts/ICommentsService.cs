namespace MyCookbook.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task CreateAsync(int recipeId, string userId, string content);

        Task AddReplyToCommentAsync(string commentId, string userId, string content);
    }
}
