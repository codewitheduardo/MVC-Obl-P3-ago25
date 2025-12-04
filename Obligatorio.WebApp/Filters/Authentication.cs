using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HTTPCLIENTE_M3C_IMEM.Filters
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;

            var token = session.GetString("Token");
            var autenticado = session.GetString("Autenticado");

            if (string.IsNullOrEmpty(token))
            {
                string msg = autenticado == "true"
                    ? "La sesión ha expirado. Por favor, inicie sesión nuevamente."
                    : "Debe iniciar sesión para continuar.";

                context.Result = new RedirectToActionResult(
                    "Login",
                    "Auth",
                    new { mensaje = msg }
                );
            }

            base.OnActionExecuting(context);
        }
    }
}
