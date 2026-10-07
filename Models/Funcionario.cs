namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Cargo { get; set; } = string.Empty;
        public Usuario Usuario { get; set; } = null;

    }
}
