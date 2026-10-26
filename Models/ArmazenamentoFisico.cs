namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public enum TipoArmazenamentoFisico{
        Hd,
        Ssd,
    }
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
        public TipoArmazenamentoFisico Tipo { get; set; }
        public int CapacidadeTotal { get; set; }
        public int QuantidadeDiscos { get; set; }
        public TipoRaid? Raid { get; set; }
        public int InfraFisicoId { get; set; }
        public InfraFisico InfraFisico { get; set; } = null!;
    }
}
