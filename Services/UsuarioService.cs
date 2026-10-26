using Calculadora_de_Plano_de_Nuvem.Data;
using Calculadora_de_Plano_de_Nuvem.Models;

namespace Calculadora_de_Plano_de_Nuvem.Services
{
    public class UsuarioService
    {
        private readonly CalculadoraContext _context;

        public UsuarioService(CalculadoraContext context)
        {
            _context = context;
        }

        public Usuario? BuscarPorEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Insira um e-mail válido.");
            }

            var resultado = _context.Usuarios.FirstOrDefault(e => e.Email.ToLower() == email.ToLower());

            if (resultado == null)
            {
                throw new Exception("Nenhum usuario encontrado.");
            }

            return resultado;
        }
    }
}
