namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public enum TipoArmazenamento
    {
        Ssd,
        Hd,
        HdMaisSsd
    }
    public class Armazenamento
    {
        public int Id { get; set; }
        public TipoArmazenamento Tipo { get; set; }
        public int CapacidadeTotal { get; set; }
        public string QuantidadeDiscos { get; set; } = string.Empty;
        public int IdInfraAtual { get; set; }
        public InfraAtual InfraAtual { get; set; } = null!;
    }
}
