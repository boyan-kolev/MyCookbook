namespace MyCookbook.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IRepliesService
    {
        Task DeleteAllByCommentsIdAsync(string commentId);
    }
}
