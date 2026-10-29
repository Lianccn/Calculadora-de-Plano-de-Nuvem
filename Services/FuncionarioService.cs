using Calculadora_de_Plano_de_Nuvem.Data;
using Calculadora_de_Plano_de_Nuvem.Dtos;
using Calculadora_de_Plano_de_Nuvem.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Calculadora_de_Plano_de_Nuvem.Services
{
    public class FuncionarioService
    {
        private readonly CalculadoraContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public FuncionarioService(CalculadoraContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        public bool Validar(FuncionarioDto funcionario, out List<ValidationResult> erros)
        {            
            var validation = new ValidationContext(funcionario);
            erros = new List<ValidationResult>();
            Validator.TryValidateObject(funcionario, validation, erros, true);
            return erros.Count == 0;

        }

        public Funcionario CadastrarFuncionario(FuncionarioDto funcionario)
        {
            if(!Validar(funcionario, out var erros))
            {
                throw new Exception(string.Join("; ", erros.Select(e => e.ErrorMessage)));                
            }

            var email = funcionario.Email.Trim().ToLowerInvariant();
            
            if(_context.Usuarios.Any(f => f.Email == email))
            {
                throw new Exception("Usuário ja está cadastrado");
            }

            var novoUsuario = new Usuario
            {
                Nome = funcionario.Nome,
                Email = funcionario.Email,
                Telefone = funcionario.Telefone
            };

            novoUsuario.SenhaHash = _passwordHasher.HashPassword(novoUsuario, funcionario.Senha);

            var novoFuncionario = new Funcionario
            {
                Cargo = funcionario.Cargo,
                Usuario = novoUsuario
            };

            _context.Funcionarios.Add(novoFuncionario);
            _context.SaveChanges();
            return novoFuncionario;
        }
    }
}
