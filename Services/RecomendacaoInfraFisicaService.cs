using Calculadora_de_Plano_de_Nuvem.Data;
using Calculadora_de_Plano_de_Nuvem.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculadora_de_Plano_de_Nuvem.Services
{
    public class RecomendacaoInfraFisicaService
    {
        private readonly CalculadoraContext _context;

        public RecomendacaoInfraFisicaService(CalculadoraContext context)
        {
            _context = context;
        }

        public async Task<Processador?> SelecionarProcessadorAsync(int infraAtualId)
        {
            var infraAtual = await _context.InfraAtuais
        .Include(i => i.Processador)
        .FirstOrDefaultAsync(i => i.Id == infraAtualId);

            if (infraAtual == null)
                throw new InvalidOperationException("Infraestrutura atual não encontrada.");

            var processadorAtual = infraAtual.Processador;

            // Primeiro tenta encontrar o modelo exato
            var processadorExato = await _context.Processadores
                .FirstOrDefaultAsync(p =>
                    p.Preco > 0 &&
                    p.Nome == processadorAtual.Nome &&
                    p.Nucleos == processadorAtual.Nucleos &&
                    p.Threads == processadorAtual.Threads);

            if (processadorExato != null)
                return processadorExato;

            // Caso não encontre, procura um equivalente ou superior
            var processadorSemelhante = await _context.Processadores
                .Where(p =>
                    p.Preco > 0 &&
                    p.Nucleos >= processadorAtual.Nucleos &&
                    p.Threads >= processadorAtual.Threads)
                .OrderBy(p => p.Nucleos - processadorAtual.Nucleos)
                .ThenBy(p => p.Threads - processadorAtual.Threads)
                .ThenBy(p => p.Preco)
                .FirstOrDefaultAsync();

            return processadorSemelhante;
        }


        public async Task<MemoriaRam?> SelecionarMemoriaRamAsync(int infraAtualId)
        {
            var infraAtual = await _context.InfraAtuais
                .FirstOrDefaultAsync(i => i.Id == infraAtualId);

            if (infraAtual == null)
            {
                throw new InvalidOperationException("Infraestrutura atual não encontrada.");
            }
                

            var memoriaNecessaria = infraAtual.Memoria;

            var memoriaRecomendada = await _context.MemoriasRam
                .Where(m =>
                    m.Tipo == TipoMemoria.DDR4 &&
                    m.CapacidadeGb >= memoriaNecessaria &&
                    m.Preco > 0)
                .OrderBy(m => m.CapacidadeGb)
                .ThenBy(m => m.Preco)
                .FirstOrDefaultAsync();

            return memoriaRecomendada;
        }



        public async Task<Disco?> SelecionarDiscoAsync(int infraAtualId)
        {
            var infraAtual = await _context.InfraAtuais
                .Include(i => i.Armazenamentos)
                .FirstOrDefaultAsync(i => i.Id == infraAtualId);

            if (infraAtual == null)
            {
                throw new InvalidOperationException("Infraestrutura atual não encontrada.");
            }
                

            var armazenamentoAtual = infraAtual.Armazenamentos.FirstOrDefault();

            if (armazenamentoAtual == null)
            {
                throw new InvalidOperationException("Armazenamento atual não encontrado.");
            }

            var tipoDisco = armazenamentoAtual.Tipo == TipoArmazenamento.Hd ? TipoDisco.Hd : TipoDisco.Ssd;

            var discos = await _context.Discos
                .Where(d =>
                       d.Tipo == tipoDisco &&
                       d.CapacidadeGb >= armazenamentoAtual.CapacidadeTotal &&
                       d.Preco > 0)
                .OrderBy(d => d.CapacidadeGb)
                .ThenBy(d => d.Preco)
                .ToListAsync();

            return discos.FirstOrDefault();
        }


        public async Task<InfraFisico> GerarInfraFisicoAsync(int infraAtualId)
        {
            // Busca a infraestrutura informada pelo cliente
            var infraAtual = await _context.InfraAtuais.Include(i => i.Armazenamentos).FirstOrDefaultAsync(i => i.Id == infraAtualId);

            if (infraAtual == null)
            {
                throw new InvalidOperationException("Infraestrutura atual não encontrada.");
            }

            // Seleciona os componentes recomendados
            var processador = await SelecionarProcessadorAsync(infraAtualId);
            var memoria = await SelecionarMemoriaRamAsync(infraAtualId);
            var disco = await SelecionarDiscoAsync(infraAtualId);

            // Verifica se todos os componentes foram encontrados
            if (processador == null || memoria == null || disco == null)
            {
                throw new InvalidOperationException("Não foi possível encontrar todos os componentes compatíveis.");
            }


            var quantidadeModulos = (int)Math.Ceiling((decimal)infraAtual.Memoria / memoria.CapacidadeGb);

            var infraFisico = new InfraFisico
            {
                TipoEquipamento = infraAtual.TipoEquipamento,
                Fabricante = infraAtual.Fabricante,
                Modelo = infraAtual.Modelo,
                ProcessadorId = processador.Id,
                Processador = processador,
                MemoriaRamId = memoria.Id,
                MemoriaRam = memoria,
                QuantidadeModulos = quantidadeModulos,
                Alimentacao = TipoAlimentacao.FontePadrao,
                Nobreak = false,
                LicencaSistema = infraAtual.SistemaOperacional,
                SuporteManutencao = SuporteManutencao.Mensal
            };

            foreach (var armazenamento in infraAtual.Armazenamentos)
            {
                infraFisico.Armazenamentos.Add(new ArmazenamentoFisico
                {
                    DiscoId = disco.Id,
                    Disco = disco,
                    QuantidadeDiscos = armazenamento.QuantidadeDiscos,
                    InfraFisico = infraFisico
                });
            }

            return infraFisico;
        }


    }
}
