namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public class CustoFisico
    {
        public int Id { get; set; }
        public decimal CustoCpu { get; set; }
        public decimal CustoMemoria { get; set; }
        public decimal CustoArmazenamento { get; set; }
        public decimal CustoInternet { get; set; }
        public decimal CustoTotal { get; set; }
        public int IdInfraFisico { get; set; }
        public InfraFisico InfraFisico { get; set; } = null!;
    }
}
