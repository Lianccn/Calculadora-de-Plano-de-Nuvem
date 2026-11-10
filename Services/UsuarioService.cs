using Calculadora_de_Plano_de_Nuvem.Data;
using Calculadora_de_Plano_de_Nuvem.Dtos;
using Calculadora_de_Plano_de_Nuvem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

        public bool ValidarUsuario(UsuarioEmpresaDto usuario, out List<ValidationResult> erros)
        {
            var validation = new ValidationContext(usuario);
            erros = new List<ValidationResult>();
            Validator.TryValidateObject(usuario, validation, erros, true);
            return erros.Count == 0;
        }

        public Usuario BuscarPorEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Insira um e-mail válido.");
            }

            var resultado = _context.Usuarios.FirstOrDefault(e => e.Email.ToLower() == email.ToLowerInvariant().Trim());

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

        public Usuario BuscarPorId(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            return usuario;
        }

        public Usuario AdicionarUsuario(UsuarioEmpresaDto usuarioDto, int id)
        {
            var empresa = BuscarPorId(id);

            if (!ValidarUsuario(usuarioDto, out var erros))
            {
                throw new Exception(string.Join("; ", erros.Select(erro => erro.ErrorMessage)));
            }

            var email = usuarioDto.Email.Trim().ToLowerInvariant();
            if (_context.Usuarios.Any(u => u.Email == email))
            {
                throw new Exception("Usuário já cadastrado");
            }

            var novoUsuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = email,
                Telefone = usuarioDto.Telefone,
                TipoUsuario = TipoUsuario.UsuarioEmpresa,
                ClienteId = empresa.Id
            };

            novoUsuario.SenhaHash = _passwordHasher.HashPassword(novoUsuario, usuarioDto.Senha);

            _context.Usuarios.Add(novoUsuario);
            _context.SaveChanges();
            return novoUsuario;
        }
    }
}
