namespace Stopify.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Stopify.Common;

    [Authorize(Roles = RoleConstants.ADMIN_ROLE)]
    public abstract class AdminController : Controller
    {

    }
}