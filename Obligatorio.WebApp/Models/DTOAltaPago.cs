namespace HTTPCLIENTE_M3C_IMEM.Models
{
    public class DTOAltaPago
    {
        public string TipoPago { get; set; }
        public string MetodoPago { get; set; }
        public int IdGasto { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaInicio { get; set; }

        //Propiedad específica para Pago Recurrente
        public DateTime FechaFin { get; set; }
    }
}
