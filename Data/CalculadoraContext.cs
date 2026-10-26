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

            modelBuilder.Entity<Solucao>()
                .HasOne(s => s.Simulacao)
                .WithOne(s => s.Solucao)
                .HasForeignKey<Solucao>(s => s.SimulacaoId);

            modelBuilder.Entity<Solucao>()
                .HasOne(s => s.InfraNuvem)
                .WithOne(i => i.Solucao)
                .HasForeignKey<Solucao>(s => s.InfraNuvemId);

            modelBuilder.Entity<Solucao>()
                .HasOne(s => s.InfraFisico)
                .WithOne(i => i.Solucao)
                .HasForeignKey<Solucao>(s => s.InfraFisicoId);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Usuario)
                .WithOne(u => u.Cliente)
                .HasForeignKey<Cliente>(c => c.UsuarioId);

            modelBuilder.Entity<Funcionario>()
                .HasOne(f => f.Usuario)
                .WithOne(u => u.Funcionario)
                .HasForeignKey<Funcionario>(f => f.UsuarioId);
        }
    }
}
