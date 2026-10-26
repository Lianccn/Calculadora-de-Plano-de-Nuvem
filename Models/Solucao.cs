namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public class Solucao
    {
        public int Id { get; set; }
        public int SimulacaoId { get; set; }
        public Simulacao Simulacao { get; set; } = null!;
        public int InfraNuvemId { get; set; }
        public InfraNuvem InfraNuvem { get; set; } = null!;
        public int InfraFisicoId { get; set; }
        public InfraFisico InfraFisico { get; set; } = null!;
    }
}
