using Calculadora_de_Plano_de_Nuvem.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculadora_de_Plano_de_Nuvem.Data
{
    public class CalculadoraContext : DbContext
    {
        public CalculadoraContext(DbContextOptions<CalculadoraContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Funcionario> Funcionarios => Set<Funcionario>();
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Simulacao> Simulacoes => Set<Simulacao>();
        public DbSet<Processador> Processadores => Set<Processador>();
        public DbSet<InfraAtual> InfraAtuais => Set<InfraAtual>();
        public DbSet<Armazenamento> Armazenamentos => Set<Armazenamento>();
        public DbSet<InfraNuvem> InfrasNuvem => Set<InfraNuvem>();
        public DbSet<CustoNuvem> CustosNuvem => Set<CustoNuvem>();
        public DbSet<InfraFisico> InfrasFisicas => Set<InfraFisico>();
        public DbSet<ArmazenamentoFisico> ArmazenamentosFisicos => Set<ArmazenamentoFisico>();
        public DbSet<CustoFisico> CustosFisicos => Set<CustoFisico>();
        public DbSet<Solucao> Solucoes => Set<Solucao>();

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Enum>().HaveConversion<string>();

            configurationBuilder.Properties<decimal>().HavePrecision(10, 2);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<Funcionario>().ToTable("funcionarios");
            modelBuilder.Entity<Cliente>().ToTable("clientes");
            modelBuilder.Entity<Simulacao>().ToTable("simulacoes");
            modelBuilder.Entity<Processador>().ToTable("processadores");
            modelBuilder.Entity<InfraAtual>().ToTable("infra_atual");
            modelBuilder.Entity<Armazenamento>().ToTable("armazenamento");
            modelBuilder.Entity<InfraNuvem>().ToTable("infra_nuvem");
            modelBuilder.Entity<CustoNuvem>().ToTable("custo_nuvem");
            modelBuilder.Entity<InfraFisico>().ToTable("infra_fisico");
            modelBuilder.Entity<ArmazenamentoFisico>().ToTable("armazenamento_fisico");
            modelBuilder.Entity<CustoFisico>().ToTable("custo_fisico");
            modelBuilder.Entity<Solucao>().ToTable("solucoes");

            // Relacionamento Simulacao - Solucao
            modelBuilder.Entity<Solucao>()
                .Property(c => c.SimulacaoId)
                .HasColumnName("id_simulacao");

            modelBuilder.Entity<Solucao>()
                .HasOne(s => s.Simulacao)
                .WithOne(s => s.Solucao)
                .HasForeignKey<Solucao>(s => s.SimulacaoId);

            // Relacionamento Infra Nuvem - Solucao
            modelBuilder.Entity<Solucao>()
               .Property(c => c.InfraNuvemId)
               .HasColumnName("id_infra_nuvem");

            modelBuilder.Entity<Solucao>()
                .HasOne(s => s.InfraNuvem)
                .WithOne(i => i.Solucao)
                .HasForeignKey<Solucao>(s => s.InfraNuvemId);

            // Relacionamento Infra Fisico - Solucao
            modelBuilder.Entity<Solucao>()
               .Property(c => c.InfraFisicoId)
               .HasColumnName("id_infra_fisico");
            
            modelBuilder.Entity<Solucao>()
                .HasOne(s => s.InfraFisico)
                .WithOne(i => i.Solucao)
                .HasForeignKey<Solucao>(s => s.InfraFisicoId);

            // Relacionamento Usuario - Cliente
            modelBuilder.Entity<Cliente>()
                .Property(c => c.UsuarioId)
                .HasColumnName("id_usuario");

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Usuario)
                .WithOne(u => u.Cliente)
                .HasForeignKey<Cliente>(c => c.UsuarioId);

            // Relacionamento Usuario - Funcionario 
            modelBuilder.Entity<Funcionario>()
                .Property(c => c.UsuarioId)
                .HasColumnName("id_usuario");

            modelBuilder.Entity<Funcionario>()
                .HasOne(f => f.Usuario)
                .WithOne(u => u.Funcionario)
                .HasForeignKey<Funcionario>(f => f.UsuarioId);

            // Relacionamento Cliente - Simulacao
            modelBuilder.Entity<Simulacao>()
                .Property(s => s.ClienteId)
                .HasColumnName("id_cliente");

            modelBuilder.Entity<Simulacao>()
                .HasOne(s => s.Cliente)
                .WithMany(c => c.Simulacoes)
                .HasForeignKey(s => s.ClienteId);

            // Relacionamento Simulacao - Infra Atual
            modelBuilder.Entity<InfraAtual>()
                .Property(i => i.SimulacaoId)
                .HasColumnName("id_simulacao");

            modelBuilder.Entity<InfraAtual>()
                .HasOne(i => i.Simulacao)
                .WithMany(s => s.InfraestruturasAtuais)
                .HasForeignKey(i => i.SimulacaoId);

            // Relacionamento Processador - InfraAtual
            modelBuilder.Entity<InfraAtual>()
                .Property(i => i.ProcessadorId)
                .HasColumnName("id_processador");

            modelBuilder.Entity<InfraAtual>()
                .HasOne(i => i.Processador)
                .WithMany(p => p.InfraestruturasAtuais)
                .HasForeignKey(i => i.ProcessadorId);

            // Relacionamento Processador - InfraFisico
            modelBuilder.Entity<InfraFisico>()
                .Property(i => i.ProcessadorId)
                .HasColumnName("id_processador");

            modelBuilder.Entity<InfraFisico>()
                .HasOne(i => i.Processador)
                .WithMany(p => p.InfraestruturasFisicas)
                .HasForeignKey(i => i.ProcessadorId);

            // Relacionamento InfraAtual - Armazenamento
            modelBuilder.Entity<Armazenamento>()
                .Property(a => a.InfraAtualId)
                .HasColumnName("id_infra_atual");

            modelBuilder.Entity<Armazenamento>()
                .HasOne(a => a.InfraAtual)
                .WithMany(i => i.Armazenamentos)
                .HasForeignKey(a => a.InfraAtualId);

            // Relacionaamento InfraFisico - ArmazenamentoFisico
            modelBuilder.Entity<ArmazenamentoFisico>()
                .Property(a => a.InfraFisicoId)
                .HasColumnName("id_infra_fisico");

            modelBuilder.Entity<ArmazenamentoFisico>()
                .HasOne(a => a.InfraFisico)
                .WithMany(i => i.Armazenamentos)
                .HasForeignKey(a => a.InfraFisicoId);

            // Relacionamento InfraNuvem - CustoNuvem
            modelBuilder.Entity<CustoNuvem>()
                .Property(c => c.InfraNuvemId)
                .HasColumnName("id_infra_nuvem");

            modelBuilder.Entity<CustoNuvem>()
                .HasOne(c => c.InfraNuvem)
                .WithOne(i => i.CustoNuvem)
                .HasForeignKey<CustoNuvem>(c => c.InfraNuvemId);

            // Relacionamento InfraFisico - CustoFisico
            modelBuilder.Entity<CustoFisico>()
                .Property(c => c.InfraFisicoId)
                .HasColumnName("id_infra_fisico");

            modelBuilder.Entity<CustoFisico>()
                .HasOne(c => c.InfraFisico)
                .WithOne(i => i.CustoFisico)
                .HasForeignKey<CustoFisico>(c => c.InfraFisicoId);
        }
    }
}
