using Calculadora_de_Plano_de_Nuvem.Data;
using Calculadora_de_Plano_de_Nuvem.Dtos;
using Calculadora_de_Plano_de_Nuvem.Models;
using Microsoft.EntityFrameworkCore;
namespace Calculadora_de_Plano_de_Nuvem.Services
{
    public class InfraAtualService
    {
        private readonly CalculadoraContext _context;

        public InfraAtualService(CalculadoraContext context)
        {
            _context = context;
        }

        public async Task<InfraAtual> CadastrarInfraAtualAsync(InfraAtualDto atualDto, int simulacaoId)
        {
            var processador = await _context.Processadores.FirstOrDefaultAsync(p =>
            p.Nome == atualDto.NomeProcessador &&
            p.Nucleos == atualDto.Nucleos &&
            p.Threads == atualDto.Threads);

            if (processador == null)
            {
                processador = new Processador
                {
                    Nome = atualDto.NomeProcessador,
                    Nucleos = atualDto.Nucleos,
                    Threads = atualDto.Threads,
                    Preco = 0
                };
                _context.Processadores.Add(processador);
                await _context.SaveChangesAsync();
            }

            var infraAtual = new InfraAtual
            {
                TipoEquipamento = atualDto.TipoEquipamento,
                Fabricante = atualDto.Fabricante,
                Modelo = atualDto.Modelo,
                Memoria = atualDto.Memoria,
                SistemaOperacional = atualDto.SistemaOperacional,
                LarguraBanda = atualDto.LarguraBanda,
                ExisteBackup = atualDto.ExisteBackup,
                FrequenciaBackup = atualDto.FrequenciaBackup,
                TempoUso = atualDto.TempoUso,
                ProcessadorId = processador.Id,
                SimulacaoId = simulacaoId
            };

            var tipoArmazenamento = atualDto.TipoArmazenamento.ToUpperInvariant() switch
            {
                "HDD" => TipoArmazenamento.Hd,
                "SSD" => TipoArmazenamento.Ssd,
                "HD e SSD" => TipoArmazenamento.HdMaisSsd,
                _=> throw new ArgumentException("Tipo de armazenamento inválido")
            };

            var armazenamento = new Armazenamento
            {
                Tipo = tipoArmazenamento,
                CapacidadeTotal = atualDto.Capacidade,
                QuantidadeDiscos = atualDto.QuantidadeDiscos,
                InfraAtual = infraAtual
            };

            infraAtual.Armazenamentos.Add(armazenamento);

            _context.InfraAtuais.Add(infraAtual);
            await _context.SaveChangesAsync();
            return infraAtual;
        }

        public async Task<InfraAtual?> BuscarPorSimulacaoAsync(int simulacaoId)
        {
            return await _context.InfraAtuais
                .Include(i => i.Processador)
                .Include(i => i.Armazenamentos)
                .FirstOrDefaultAsync(i => i.SimulacaoId == simulacaoId);
        }
    }
}
