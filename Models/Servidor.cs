using System.Net;

namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public enum SistemaOperacional
    {
        Windows,
        Linux
    }

    public enum TipoRaid
    {
        Nenhum,
        Raid0,
        Raid1,
        Raid5,
        Raid10
    }

    public class Servidor
    {
        public int Id { get; set; }
        public IPAddress Ip { get; set; } 
        public int Cpu { get; set; }
        public int MemoriaGb { get; set; }
        public int ArmazenamentoGb { get; set; }
        public TipoRaid Raid { get; set; }
        public bool LicencaSistema { get; set; }
        public SistemaOperacional SistemaOperacional { get; set; }
    }

    public class ServidorNuvem : Servidor
    {
        public string TipoNuvem { get; set; } = string.Empty;
    }

    public class ServidorFisico : Servidor
    {
        public string FonteAlimentacao { get; set; } = string.Empty;
        public bool ManutencaoPreventiva { get; set; }
    }
}