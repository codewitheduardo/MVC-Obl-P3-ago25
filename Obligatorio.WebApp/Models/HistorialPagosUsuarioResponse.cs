namespace HTTPCLIENTE_M3C_IMEM.Models
{
    public class HistorialPagosUsuarioResponse
    {
        public string MetodoPago { get; set; }
        public string Gasto { get; set; }
        public decimal MontoFinal { get; set; }
        public string TipoPago { get; set; }
        public decimal SaldoPendiente { get; set; }

        // Propiedad compartida
        public string FechaInicio { get; set; }

        // Propiedad específica para Pago Recurrente
        public string FechaFin { get; set; }
    }
}
