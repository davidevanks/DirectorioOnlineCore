using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AppDirectorioWeb.Utiles.CustomAttributes
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        #region Public Constructors

        public AuthorizeAttribute(params string[] claim) : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[] { claim };
        }

        #endregion Public Constructors
    }

    public class AuthorizeFilter : IAuthorizationFilter
    {
        #region Private Fields

        private readonly string[] _claim;

        #endregion Private Fields

        #region Public Constructors

        public AuthorizeFilter(params string[] claim)
        {
            _claim = claim;
        }

        #endregion Public Constructors

        #region Public Methods

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            var claimsIndentity = context.HttpContext.User.Identity as ClaimsIdentity;

            if (IsAuthenticated)
            {
                bool flagClaim = false;
                foreach (var item in _claim)
                {
                    if (context.HttpContext.User.HasClaim(item, item))
                        flagClaim = true;
                }
                if (!flagClaim)
                {
                    context.Result = new RedirectResult("~/Home/Error");
                }
            }
            else
            {
                context.Result = new RedirectResult("~/Account/Login");
            }
            return;
        }

        #endregion Public Methods
    }
}