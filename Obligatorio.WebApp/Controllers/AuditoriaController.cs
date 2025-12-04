using HTTPCLIENTE_M3C_IMEM.Filters;
using HTTPCLIENTE_M3C_IMEM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace HTTPCLIENTE_M3C_IMEM.Controllers
{
    public class AuditoriaController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuditoriaController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        [Authentication]
        [AdminFilter]
        public async Task<IActionResult> AuditoriaPorGasto()
        {
            try
            {
                var vm = new AuditoriaGastoVM();

                var token = HttpContext.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var gastosResponse = await _httpClient.GetAsync("Gasto/Activos");
                vm.TodosLosGastos = await gastosResponse.Content.ReadFromJsonAsync<List<DTOGasto>>();

                return View(vm);
            } catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error al cargar la página. Por favor, intente más tarde.";
                return View();
            }
        }

        [Authentication]
        [AdminFilter]
        [HttpPost]
        public async Task<IActionResult> AuditoriaPorGasto(AuditoriaGastoVM vm)
        {
            try
            {
                var token = HttpContext.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var gastosResponse = await _httpClient.GetAsync("Gasto/Activos");
                vm.TodosLosGastos = await gastosResponse.Content.ReadFromJsonAsync<List<DTOGasto>>();

                var auditoriaResponse = await _httpClient.GetAsync($"Auditoria/Gasto/{vm.IdGastoSeleccionado}");

                if (auditoriaResponse.IsSuccessStatusCode)
                {
                    vm.Auditorias = await auditoriaResponse.Content.ReadFromJsonAsync<List<DTOAuditoria>>();

                    return View(vm);
                }
                else
                {
                    ViewBag.Error = "No se pudieron obtener los registros de auditoría para el gasto seleccionado.";

                    return View(vm);
                }
            } catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error al procesar la solicitud. Por favor, intente más tarde.";
                return View(vm);
            }
        }
    }
}
