using Calculadora_de_Plano_de_Nuvem.Data;
using Calculadora_de_Plano_de_Nuvem.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculadora_de_Plano_de_Nuvem.Services
{
    public class SimulacaoService
    {
        private readonly CalculadoraContext _context;

        public SimulacaoService(CalculadoraContext context)
        {
            _context = context;
        }

        public async Task<Simulacao> CadastrarSimulacaoAsync(int clienteId, PeriodoUso periodoUso)
        {
            var simulacao = new Simulacao
            {
                DataSimulacao = DateTime.Now,
                PeriodoUso = periodoUso,
                ClienteId = clienteId
            };

            _context.Simulacoes.Add(simulacao);

            var entradas = _context.ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added)
                .Select(e => $"{e.Entity.GetType().Name}: {e.State}")
                .ToList();

            Console.WriteLine("ENTIDADES PENDENTES:");

            foreach (var entrada in entradas)
            {
                Console.WriteLine(entrada);
            }

            await _context.SaveChangesAsync();

            return simulacao;
        }
    }
}
