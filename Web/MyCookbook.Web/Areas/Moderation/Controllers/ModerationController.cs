namespace MyCookbook.Web.Areas.Moderation.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Common;
    using MyCookbook.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
    [Area("Moderation")]
    public class ModerationController : BaseController
    {
    }
}
