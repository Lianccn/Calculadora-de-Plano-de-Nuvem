namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public class Solucao
    {
        public int Id { get; set; }
        public int IdSimulacao { get; set; }
        public Simulacao Simulacao { get; set; } = null!;
        public int IdInfraNuvem { get; set; }
        public InfraNuvem InfraNuvem { get; set; } = null!;
        public int IdInfraFisico { get; set; }
        public InfraFisico InfraFisico { get; set; } = null!;
    }
}
