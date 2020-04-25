namespace MyCookbook.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyCookbook.Data.Common.Repositories;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;

    public class RepliesService : IRepliesService
    {
        private readonly IDeletableEntityRepository<Reply> repliesRepository;

        public RepliesService(IDeletableEntityRepository<Reply> repliesRepository)
        {
            this.repliesRepository = repliesRepository;
        }

        public async Task DeleteAllByCommentsIdAsync(string commentId)
        {
            var replies = this.repliesRepository
                .All()
                .Where(r => r.CommentId == commentId)
                .ToList();

            foreach (var reply in replies)
            {
                this.repliesRepository.Delete(reply);
            }

            await this.repliesRepository.SaveChangesAsync();
        }
    }
}
