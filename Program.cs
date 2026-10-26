using Calculadora_de_Plano_de_Nuvem.Components;
using Calculadora_de_Plano_de_Nuvem.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("Banco");

builder.Services.AddDbContext<CalculadoraContext>(options => options.UseMySQL(connectionString!).UseSnakeCaseNamingConvention());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CalculadoraContext>();
    Console.WriteLine($"Conecta: {context.Database.CanConnect()}");
    _ = context.Usuarios.Count();
    _ = context.Funcionarios.Count();
    _ = context.Clientes.Count();
    _ = context.Simulacoes.Count();
    _ = context.Processadores.Count();
    _ = context.InfraAtuais.Count();
    _ = context.Armazenamentos.Count();
    _ = context.InfrasNuvem.Count();
    _ = context.CustosNuvem.Count();
    _ = context.InfrasFisicas.Count();
    _ = context.ArmazenamentosFisicos.Count();
    _ = context.CustosFisicos.Count();
    _ = context.Solucoes.Count();
    Console.WriteLine("Todas as tabelas respondem.");
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
