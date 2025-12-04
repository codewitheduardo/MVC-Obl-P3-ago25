namespace HTTPCLIENTE_M3C_IMEM.Models
{
    public class AuditoriaGastoVM
    {
        public int IdGastoSeleccionado { get; set; }
        public List<DTOGasto> TodosLosGastos { get; set; } = new List<DTOGasto>();
        public List<DTOAuditoria> Auditorias { get; set; } = new List<DTOAuditoria>();
    }
}
