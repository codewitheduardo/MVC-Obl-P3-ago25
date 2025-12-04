using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HTTPCLIENTE_M3C_IMEM.Filters
{
    public class GerenteFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string? rol = context.HttpContext.Session.GetString("Rol");

            if (rol != "GERENTE")
            {
                context.Result = new RedirectToActionResult("Index", "Home", new { mensaje = "Permiso insuficiente." });
            }
            base.OnActionExecuted(context);
        }
    }
}
