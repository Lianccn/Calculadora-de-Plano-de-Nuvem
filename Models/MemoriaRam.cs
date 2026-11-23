namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public enum TipoMemoria
    {
        DDR3,
        DDR4,
        DDR5
    }
    public class MemoriaRam
    {
        public int Id { get; set; }
        public string Fabricante { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public TipoMemoria Tipo { get; set; }
        public int CapacidadeGb { get; set; }
        public int FrequenciaMhz { get; set; }
        public decimal Preco { get; set; }
    }
}
