namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public enum TipoEquipamento
    {
        Servidor,
        Desktop,
        Notebook
    }
    public enum FrequenciaBackup
    {
        Diario,
        Semanal,
        Mensal,
        Quinzenal,
        Anual
    }
    public class InfraAtual
    {
        public int Id { get; set; }
        public TipoEquipamento TipoEquipamento { get; set; }
        public string Fabricante { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Memoria { get; set; }
        public string SistemaOperacional { get; set; } = string.Empty;
        public int LarguraBanda { get; set; }
        public bool ExisteBackup { get; set; }
        public FrequenciaBackup? FrequenciaBackup { get; set; }
        public PeriodoUso TempoUso { get; set; }
        public int ProcessadorId { get; set; }
        public Processador Processador { get; set; } = null!;
        public int SimulacaoId { get; set; }
        public Simulacao Simulacao { get; set; } = null!;
        public List<Armazenamento> Armazenamentos { get; set; } = new();

    }
}
