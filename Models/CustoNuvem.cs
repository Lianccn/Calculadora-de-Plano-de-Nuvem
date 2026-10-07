namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public class CustoNuvem
    {
        public int Id { get; set; }
        public decimal CustoCpu { get; set; }
        public decimal CustoMemoria { get; set; }
        public decimal CustoArmazenamento { get; set; }
        public decimal CustoBackup { get; set; }
        public decimal CustoBanda { get; set; }
        public decimal CustoSistemaOperacional { get; set; }
        public decimal CustoMensal { get; set; }
        public int IdInfraNuvem { get; set; }
        public InfraNuvem InfraNuvem { get; set; } = null!;
    }
}
