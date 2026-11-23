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
        public int QuantidadeDiscos { get; set; }
        public TipoRaid? Raid { get; set; }
        public int DiscoId { get; set; }
        public Disco Disco { get; set; } = null!;
        public int InfraFisicoId { get; set; }
        public InfraFisico InfraFisico { get; set; } = null!;
    }
}
