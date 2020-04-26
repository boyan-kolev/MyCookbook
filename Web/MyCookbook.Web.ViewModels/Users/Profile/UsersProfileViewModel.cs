using System;

namespace MyCookbook.Web.ViewModels.Users.Profile
{
    public class UsersProfileViewModel
    {
        public string FullName { get; set; }

        public DateTime Birthdate { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Email { get; set; }

        public int MyProperty { get; set; }
    }
}
