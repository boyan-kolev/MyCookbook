namespace MyCookbook.Web.ViewModels.Administration.Moderators.Add
{
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class UsersListViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePhoto { get; set; }

        public bool Selected { get; set; }
    }
}
