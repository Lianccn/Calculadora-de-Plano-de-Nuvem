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
        public string SenhaHash { get; set; } = string.Empty;
        public Funcionario? Funcionario { get; set; }
        public Cliente? Cliente { get; set; }

    }
}
