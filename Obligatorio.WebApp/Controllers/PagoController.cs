using HTTPCLIENTE_M3C_IMEM.Filters;
using HTTPCLIENTE_M3C_IMEM.Models;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HTTPCLIENTE_M3C_IMEM.Controllers
{
    public class PagoController : Controller
    {
        private readonly HttpClient _httpClient;

        public PagoController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        [Authentication]
        [EmpleadoGerenteFilter]
        public async Task<IActionResult> HistorialPagosUsuario()
        {
            try
            {
                var token = HttpContext.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync("Pago/Usuario");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var historialPagosUsuarioResponse = JsonSerializer.Deserialize<List<HistorialPagosUsuarioResponse>>(result, options);

                    return View(historialPagosUsuarioResponse);
                }
                else
                {
                    TempData["Error"] = "Error al obtener el historial de pagos. Por favor, intente más tarde.";

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception e)
            {
                TempData["Error"] = "Ocurrió un error inesperado: " + e.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [Authentication]
        public async Task<IActionResult> Create()
        {
            try
            {
                var vm = new AltaPagoVM();

                var token = HttpContext.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var gastosResponse = await _httpClient.GetAsync("Gasto/Activos");
                vm.TodosLosGastos = await gastosResponse.Content.ReadFromJsonAsync<List<DTOGasto>>();

                var metodosResponse = await _httpClient.GetAsync("MetodoPago");
                vm.TodosLosMetodosDePago = await metodosResponse.Content.ReadFromJsonAsync<List<DTOMetodoPago>>();

                return View(vm);
            }
            catch (Exception e)
            {
                TempData["Error"] = "Error al cargar el formulario de pago. Por favor, intente más tarde.";

                return RedirectToAction("Index", "Home");
            }
            }

        [Authentication]
        [HttpPost]
        public async Task<IActionResult> Create(DTOAltaPago dto)
        {
            try
            {
                var token = HttpContext.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var body = JsonSerializer.Serialize(dto);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Pago/Alta", content);

                var vm = new AltaPagoVM();

                var gastosResponse = await _httpClient.GetAsync("Gasto/Activos");
                vm.TodosLosGastos = await gastosResponse.Content.ReadFromJsonAsync<List<DTOGasto>>();

                var metodosResponse = await _httpClient.GetAsync("MetodoPago");
                vm.TodosLosMetodosDePago = await metodosResponse.Content.ReadFromJsonAsync<List<DTOMetodoPago>>();

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Success = "Pago registrado exitosamente.";

                    ModelState.Clear();
                    vm.Dto = new DTOAltaPago();

                    return View(vm);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ViewBag.Error = "Datos inválidos. Revise los campos e intente de nuevo.";
                    vm.Dto = dto;

                    return View(vm);

                } else
                {
                    ViewBag.Error = "Error al registrar el pago. Por favor, intente más tarde.";
                    vm.Dto = dto;

                    return View(vm);
                }
            } catch (Exception e)
            {
                ViewBag.Error = "Ocurrió un error inesperado: " + e.Message;

                return View();
            }
        }
    }
}
