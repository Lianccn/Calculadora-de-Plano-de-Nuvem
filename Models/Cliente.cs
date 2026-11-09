namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string NomeEmpresa { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public List<Usuario> Usuarios { get; set; } = new();
        public List<Simulacao> Simulacoes { get; set; } = new();
    }
}
