namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public enum PeriodoUso
    {
        MenosDe1Ano,
        De1a3Anos,
        De3a5Anos,
        MaisDe5Anos
    }
    public class Simulacao
    {
        public int Id { get; set; }
        public DateTime DataSimulacao { get; set; }
        public PeriodoUso PeriodoUso { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; } = null;
        public List<InfraAtual> InfraestruturasAtuais { get; set; } = new();
        public List<Solucao> Solucoes { get; set; } = new();
    }
}
