using System.Net;

namespace Calculadora_de_Plano_de_Nuvem.Models
{
    
    public class InfraNuvem
    {
        public int Id { get; set; }
        public int CpuNucleos { get; set; }
        public int Memoria { get; set; }
        public int Armazenamento { get; set; }
        public int QuantidadeBackup { get; set; }
        public int TransferenciaBanda { get; set; }
        public string SistemaOperacional { get; set; } = string.Empty;
        public CustoNuvem? CustoNuvem { get; set; }
        public List<Solucao> Solucoes { get; set; } = new();
    }
}
