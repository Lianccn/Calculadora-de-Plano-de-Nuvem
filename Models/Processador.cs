namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public class Processador
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Nucleos { get; set; }
        public int Threads { get; set; }
        public List<InfraAtual> InfraestruturasAtuais { get; set; } = new();
        public List<InfraFisico> InfraestruturaFisicas { get; set; } = new();
    }
}
