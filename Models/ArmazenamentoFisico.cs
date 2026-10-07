namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public enum TipoRaid
    {
        Raid0,
        Raid1,
        Raid5,
        Raid6,
        Raid10,
        RaidHibrido
    }

    public class ArmazenamentoFisico
    {
        public int Id { get; set; }
        public TipoArmazenamento Tipo { get; set; }
        public int CapacidadeTotal { get; set; }
        public string QuantidadeDiscos { get; set; } = string.Empty;
        public TipoRaid? Raid { get; set; }
        public int IdInfraFisico { get; set; }
        public InfraFisico InfraFisico { get; set; } = null!;
    }
}
