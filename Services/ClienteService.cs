using Calculadora_de_Plano_de_Nuvem.Data;
using Calculadora_de_Plano_de_Nuvem.Dtos;
using Calculadora_de_Plano_de_Nuvem.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Calculadora_de_Plano_de_Nuvem.Services
{
    public class ClienteService
    {
        private readonly CalculadoraContext _context;

        public ClienteService(CalculadoraContext context)
        {
            _context = context;
        }
        public bool Validar (ClienteDto cliente, out List<ValidationResult> erros)
        {
            var validation = new ValidationContext(cliente);
            erros = new List<ValidationResult>();
            Validator.TryValidateObject(cliente, validation, erros, true);
            return erros.Count == 0;
        }

        private readonly PasswordHasher<Usuario> _passwordHasher = new();
        public Cliente CadastrarCliente(ClienteDto cliente)
        {
            if(!Validar(cliente, out var erros))
            {
                throw new Exception(string.Join("; ", erros.Select(e => e.ErrorMessage)));
            }

            if(_context.Usuarios.Any(c => c.Email == cliente.Email) || _context.Clientes.Any(c => c.Cnpj == cliente.Cnpj))
            {
                throw new Exception("Usuário ou empresa já cadastrados");
            }

            var novoUsuario = new Usuario
            {
                Nome = cliente.Nome,
                Email = cliente.Email,
                Telefone = cliente.Telefone,
            };

            novoUsuario.SenhaHash = _passwordHasher.HashPassword(novoUsuario, cliente.Senha);

            var novoCliente = new Cliente
            {
                NomeEmpresa = cliente.NomeEmpresa,
                Cnpj = cliente.Cnpj,
                Usuario = novoUsuario
            };

            _context.Clientes.Add(novoCliente);
            _context.SaveChanges();
            return novoCliente;

        }
    }
}
