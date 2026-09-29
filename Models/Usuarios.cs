namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public enum TipoUsuario
    {
        Cliente,
        Administrador
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public TipoUsuario Tipo { get; set; }
        public List<PlanoNuvem> Planos { get; set; } = new();
    }
}
