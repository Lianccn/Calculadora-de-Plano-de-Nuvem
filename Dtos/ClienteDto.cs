using System.ComponentModel.DataAnnotations;

namespace Calculadora_de_Plano_de_Nuvem.Dtos
{
    public class ClienteDto
    {
        [Required(ErrorMessage = "Informe o nome.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o email.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o telefone.")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a senha.")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirme a senha")]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não são iguais.")]
        public string ConfirmarSenha { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o nome da empresa.")]
        public string NomeEmpresa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o CNPJ da empresa.")]
        public string Cnpj { get; set; } = string.Empty;    

    }
}
