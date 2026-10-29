using Calculadora_de_Plano_de_Nuvem.Components;
using Calculadora_de_Plano_de_Nuvem.Data;
using Calculadora_de_Plano_de_Nuvem.Dtos;
using Calculadora_de_Plano_de_Nuvem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("Banco");

builder.Services.AddDbContext<CalculadoraContext>(options => options.UseMySQL(connectionString!).UseSnakeCaseNamingConvention());

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<FuncionarioService>();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/acesso-negado";
        options.Cookie.Name = "CalculadoraNuvem.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

app.MapPost("/auth/login", async (
    HttpContext context,
    IAntiforgery antiforgery,
    UsuarioService usuarioService) =>
{
    await antiforgery.ValidateRequestAsync(context);

    var form = await context.Request.ReadFormAsync();

    var email = form["Email"].ToString().Trim();
    var senha = form["Senha"].ToString();
    var lembrar = form["Lembrar"] == "on";

    if (!usuarioService.ValidarLogin(email, senha))
    {
        return Results.Redirect("/login?erro=credenciais");
    }

    var usuario = usuarioService.BuscarPorEmail(email);

    var claims = new List<Claim>
    {
        new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new(ClaimTypes.Name, usuario.Nome),
        new(ClaimTypes.Email, usuario.Email)
    };

    var identidade = new ClaimsIdentity(
        claims,
        CookieAuthenticationDefaults.AuthenticationScheme);

    var principal = new ClaimsPrincipal(identidade);

    await context.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        principal,
        new AuthenticationProperties
        {
            IsPersistent = lembrar,
            AllowRefresh = true
        });

    return Results.Redirect("/");
});

// CÓDIGO USADO PARA TESTE COM BANCO
//using (var scope = app.Services.CreateScope())
//{
//    var clienteService = scope.ServiceProvider
//        .GetRequiredService<ClienteService>();

//    var clienteTeste = new ClienteDto
//    {
//        Nome = "Usuario Teste",
//        Email = "teste.cadastro@example.com",
//        Telefone = "14999999999",
//        Senha = "SenhaTeste123",
//        ConfirmarSenha = "SenhaTeste123",
//        NomeEmpresa = "Empresa Teste",
//        Cnpj = "12345678000199"
//    };

//    try
//    {
//        var clienteCadastrado =
//            clienteService.CadastrarCliente(clienteTeste);

//        Console.WriteLine("Cadastro realizado com sucesso!");
//        Console.WriteLine($"ID do cliente: {clienteCadastrado.Id}");
//        Console.WriteLine($"ID do usuário: {clienteCadastrado.UsuarioId}");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Erro no cadastro: {ex.Message}");
//    }
//}

// CÓDIGO PARA TESTAR A VALIDADE DO LOGIN
//using (var scope = app.Services.CreateScope())
//{
//    var usuarioService = scope.ServiceProvider
//        .GetRequiredService<UsuarioService>();

//    var loginValido = usuarioService.ValidarLogin(
//        "teste.cadastro@example.com",
//        "SenhaTeste123"
//    );

//    Console.WriteLine($"Login válido: {loginValido}");

//    var senhaIncorreta = usuarioService.ValidarLogin(
//        "teste.cadastro@example.com",
//        "SenhaErrada"
//    );

//    Console.WriteLine($"Senha incorreta aceita: {senhaIncorreta}");
//}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.MapPost("/auth/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(
        CookieAuthenticationDefaults.AuthenticationScheme);

    return Results.Redirect("/login");
});


app.Run();
