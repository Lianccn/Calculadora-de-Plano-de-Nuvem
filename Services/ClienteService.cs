using Calculadora_de_Plano_de_Nuvem.Data;
using Calculadora_de_Plano_de_Nuvem.Dtos;
using Calculadora_de_Plano_de_Nuvem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using System.ComponentModel.DataAnnotations;

namespace Calculadora_de_Plano_de_Nuvem.Services
{
    public class ClienteService
    {
        private readonly CalculadoraContext _context;

        public ClienteService(CalculadoraContext context)
        {
            _context = context;
        }
        public bool ValidarCliente (ClienteDto cliente, out List<ValidationResult> erros)
        {
            var validation = new ValidationContext(cliente);
            erros = new List<ValidationResult>();
            Validator.TryValidateObject(cliente, validation, erros, true);
            return erros.Count == 0;
        }
        private readonly PasswordHasher<Usuario> _passwordHasher = new();
        public Cliente CadastrarCliente(ClienteDto cliente)
        {
            if(!ValidarCliente(cliente, out var erros))
            {
                throw new Exception(string.Join("; ", erros.Select(erro => erro.ErrorMessage)));
            }

            var email = cliente.Email.Trim().ToLowerInvariant();
            var cnpj = cliente.Cnpj.Trim();

            if(_context.Usuarios.Any(cliente => cliente.Email == email) || _context.Clientes.Any(cliente => cliente.Cnpj == cnpj))
            {
                throw new Exception("Empresa já cadastrados");
            }

            var novoUsuario = new Usuario
            {
                Nome = cliente.Nome,
                Email = email,
                Telefone = cliente.Telefone,
                TipoUsuario = TipoUsuario.Cliente
            };

            novoUsuario.SenhaHash = _passwordHasher.HashPassword(novoUsuario, cliente.Senha);

            var novoCliente = new Cliente
            {
                NomeEmpresa = cliente.NomeEmpresa,
                Cnpj = cnpj
            };

            novoUsuario.Cliente = novoCliente;

            _context.Clientes.Add(novoCliente);
            _context.SaveChanges();
            return novoCliente;

        }

        public List<Cliente> ListarClientes()
        {
            return _context.Clientes.Include(cliente => cliente.Usuarios).ToList();            
        }

        public Cliente BuscarPorId(int id)
        {
            if(id <= 0)
            {
                throw new InvalidOperationException("Id inválido.");
            }

            Cliente? cliente = _context.Clientes.Include(cliente => cliente.Usuarios).FirstOrDefault(cliente => cliente.Id == id);

            if(cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }

            return cliente;
        }        
    }
}
