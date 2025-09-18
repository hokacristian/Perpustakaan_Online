using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Perpustakaan_Online.Controllers
{
    public class BaseController : Controller
    {
        protected int? CurrentUserId
        {
            get
            {
                var userIdString = HttpContext.Session.GetString("UserId");
                if (int.TryParse(userIdString, out int userId))
                {
                    return userId;
                }
                return null;
            }
        }

        protected string? CurrentUserEmail => HttpContext.Session.GetString("UserEmail");
        protected string? CurrentUserName => HttpContext.Session.GetString("UserName");
        protected string? CurrentUserRole => HttpContext.Session.GetString("UserRole");

        protected bool IsAuthenticated => CurrentUserId.HasValue;
        protected bool IsAdmin => CurrentUserRole == "Admin";
        protected bool IsUser => CurrentUserRole == "User";

        protected IActionResult RedirectToLogin()
        {
            return RedirectToAction("Login", "Account");
        }

        protected IActionResult RedirectToAccessDenied()
        {
            return RedirectToAction("AccessDenied", "Account");
        }
    }

    public class RequireAuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userIdString = context.HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }
    }

    public class RequireAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userIdString = context.HttpContext.Session.GetString("UserId");
            var userRole = context.HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(userIdString))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
            else if (userRole != "Admin")
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
            }
        }
    }
}