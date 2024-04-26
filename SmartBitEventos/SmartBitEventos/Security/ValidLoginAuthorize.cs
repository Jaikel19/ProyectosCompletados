using DTO.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Http.Headers;

namespace SmartBitEventos.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ValidLoginAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public EnumAccess Access { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (string.IsNullOrEmpty(context.HttpContext.Request.Headers["Authorization"])) Unauthorized(context);

            var header = AuthenticationHeaderValue.Parse(context.HttpContext.Request.Headers["Authorization"]);
            var credentials = header.Parameter;

            if (string.IsNullOrEmpty(credentials)) credentials = header.Scheme;

            var userActivo = Security.Authentication.ValidateToken(credentials);
            
            if (userActivo == null)
            {
                Unauthorized(context);
            }

            var approved = userActivo.Accesos.Where(x => x.Nombre == Access.ToString()).Any();
            if (!approved) 
            {
                Unauthorized(context);
            }
        }

        private void Unauthorized(AuthorizationFilterContext context)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}
