namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public enum TipoDisco
    {
        Hd,
        Ssd
    }

    public class Disco
    {
        public int Id { get; set; }
        public string Fabricante { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public TipoDisco Tipo { get; set; }
        public int CapacidadeGb { get; set; }
        public decimal Preco { get; set; }
    }
}
