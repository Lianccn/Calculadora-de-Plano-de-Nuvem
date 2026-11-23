using Calculadora_de_Plano_de_Nuvem.Models;

namespace Calculadora_de_Plano_de_Nuvem.Dtos
{
    public class InfraAtualDto
    {
        public TipoEquipamento TipoEquipamento { get; set; }
        public string Fabricante { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string NomeProcessador { get; set; } = string.Empty;
        public int Nucleos { get; set; }
        public int Threads { get; set; }
        public int Memoria { get; set; }
        public string SistemaOperacional { get; set; } = string.Empty;
        public int LarguraBanda { get; set; }
        public bool ExisteBackup { get; set; }
        public FrequenciaBackup? FrequenciaBackup { get; set; }
        public PeriodoUso TempoUso { get; set; }
        public string TipoArmazenamento { get; set; } = string.Empty;
        public int Capacidade { get; set; }
        public int QuantidadeDiscos { get; set; }
        public string? Raid { get; set; }
    }
}
