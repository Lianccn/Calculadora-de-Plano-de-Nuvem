using Calculadora_de_Plano_de_Nuvem.Data;
using Calculadora_de_Plano_de_Nuvem.Dtos;
using Calculadora_de_Plano_de_Nuvem.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Calculadora_de_Plano_de_Nuvem.Services
{
    public class AdministradorService
    {
        private readonly CalculadoraContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public AdministradorService(CalculadoraContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        public bool Validar(AdministradorDto admin, out List<ValidationResult> erros)
        {
            var validation = new ValidationContext(admin);
            erros = new List<ValidationResult>();
            Validator.TryValidateObject(admin, validation, erros, true);
            return erros.Count == 0;
        }

        public Usuario CadastrarAdmin(AdministradorDto admin)
        {
            if (!Validar(admin, out var erros))
            {
                throw new Exception(string.Join("; ", erros.Select(e => e.ErrorMessage)));
            }

            if (_context.Usuarios.Any(u => u.TipoUsuario == TipoUsuario.Administrador))
            {
                throw new Exception("Adiminstrador já está cadastrado.");
            }

            var email = admin.Email.Trim().ToLowerInvariant();

            if (_context.Usuarios.Any(u =>u.Email.ToLower() == email))
            {
                throw new InvalidOperationException("Este e-mail já está cadastrado.");
            }

            var usuario = new Usuario()
            {
                Nome = admin.Nome.Trim(),
                Email = email,
                Telefone = admin.Telefone.Trim(),
                TipoUsuario = TipoUsuario.Administrador
            };

            usuario.SenhaHash = _passwordHasher.HashPassword(usuario, admin.Senha);

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }
    }
}
