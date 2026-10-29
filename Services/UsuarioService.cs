using Calculadora_de_Plano_de_Nuvem.Data;
using Calculadora_de_Plano_de_Nuvem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Calculadora_de_Plano_de_Nuvem.Services
{
    public class UsuarioService
    {
        private readonly CalculadoraContext _context;

        public UsuarioService(CalculadoraContext context)
        {
            _context = context;
        }

        private readonly PasswordHasher<Usuario> _passwordHasher = new();

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

        public bool ValidarLogin(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                return false;
            }

            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email.ToLower() == email.Trim().ToLower());

            if (usuario == null)
            {
                return false;
            }

            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.SenhaHash, senha);

            return resultado != PasswordVerificationResult.Failed;
        }
    }
}
