using HTTPCLIENTE_M3C_IMEM.Filters;
using HTTPCLIENTE_M3C_IMEM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HTTPCLIENTE_M3C_IMEM.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly HttpClient _httpClient;

        public UsuarioController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        [Authentication]
        [AdminFilter]
        public IActionResult ResetPassword()
        {
            return View(new DTOResetPassword());
        }

        [Authentication]
        [AdminFilter]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(DTOResetPassword dto)
        {
            try
            {
                var token = HttpContext.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var body = JsonSerializer.Serialize(dto);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Usuario/ResetearPassword", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var newPass = result;

                    ViewBag.NewPassword = newPass;

                    return View(dto);
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ViewBag.Error = "Usuario no encontrado. Por favor, verifique el correo.";
                    }
                    else
                    {
                        ViewBag.Error = "Error al restablecer la contraseña. Por favor, intente más tarde.";
                    }

                    return View(dto);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Ocurrió un error: {ex.Message}";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
