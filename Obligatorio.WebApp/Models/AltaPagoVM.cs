namespace HTTPCLIENTE_M3C_IMEM.Models
{
    public class AltaPagoVM
    {
        public DTOAltaPago Dto { get; set; } = new DTOAltaPago();
        public List<DTOGasto> TodosLosGastos { get; set; } = new List<DTOGasto>();
        public List<DTOMetodoPago> TodosLosMetodosDePago { get; set; } = new List<DTOMetodoPago>();
    }
}
