using HTTPCLIENTE_M3C_IMEM.Filters;
using HTTPCLIENTE_M3C_IMEM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace HTTPCLIENTE_M3C_IMEM.Controllers
{
    public class EquipoController : Controller
    {
        private readonly HttpClient _httpClient;

        public EquipoController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        [GerenteFilter]
        public IActionResult EquiposPagosUnicosMayorMonto()
        {
            return View(new DTOMonto());
        }

        [GerenteFilter]
        [HttpPost]
        public async Task<IActionResult> EquiposPagosUnicosMayorMonto(DTOMonto dto)
        {
            try
            {
                var token = HttpContext.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"Equipo/PagosUnicosMayores?monto={dto.Monto}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var equiposResponse = System.Text.Json.JsonSerializer.Deserialize<List<EquipoPagoUnicoResponse>>(result, options);

                    ViewBag.Equipos = equiposResponse;

                    return View(dto);
                }
                else
                {
                    ViewBag.Error = "No se pudieron obtener los equipos con pagos únicos mayores al monto especificado.";

                    return View(new DTOMonto());
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
