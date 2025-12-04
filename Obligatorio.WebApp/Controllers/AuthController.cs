using HTTPCLIENTE_M3C_IMEM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace HTTPCLIENTE_M3C_IMEM.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(DTOLogin dto)
        {
            try
            {
                var body = JsonSerializer.Serialize(dto);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Auth", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var loginResponse = JsonSerializer.Deserialize<LoginResponse>(result, options);

                    HttpContext.Session.SetString("Token", loginResponse.Token);
                    HttpContext.Session.SetString("Rol", loginResponse.Rol);
                    HttpContext.Session.SetString("Nombre", loginResponse.Nombre);
                    HttpContext.Session.SetString("Autenticado", "true");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        ViewBag.Error = "Credenciales incorrectas. Por favor, intente de nuevo.";
                    }
                    else
                    {
                        ViewBag.Error = "Ocurrió un error al iniciar sesión. Por favor, intente más tarde.";
                    }

                    ModelState.Clear();
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Ocurrió un error inesperado: " + e.Message;
                ModelState.Clear();

                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Auth");

        }
    }
}
