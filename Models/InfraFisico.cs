namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public enum TipoAlimentacao
    {
        FontePadrao,
        FonteRedundante
    }

    public enum TipoNobreak
    {
        Sim,
        Nao
    }

    public enum SuporteManutencao
    {
        Semanal,
        Quinzenal,
        Mensal,
        Trimestral,
        Semestral,
        Anual
    }

    public class InfraFisico
    {
        public int Id { get; set; }
        public TipoEquipamento TipoEquipamento { get; set; }
        public string Fabricante { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Memoria { get; set; }
        public TipoAlimentacao Alimentacao { get; set; }
        public TipoNobreak Nobreak { get; set; }
        public string LicencaSistema { get; set; } = string.Empty;
        public SuporteManutencao? SuporteManutencao { get; set; }
        public int IdProcessador { get; set; }
        public Processador Processador { get; set; } = null!;
        public List<ArmazenamentoFisico> Armazenamentos { get; set; } = new();
        public CustoFisico? CustoFisico { get; set; }
        public List<Solucao> Solucoes { get; set; } = new();
    }
}
