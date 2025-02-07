using APIGuia;
using APIGuia.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddAuthorization();

// Convertendo a chave de segurança em um array de bytes para ser usada na geração do token JWT
var key = Encoding.ASCII.GetBytes(Settings.secret);

// Configuração do JWT
builder.Services.AddAuthentication(x =>
    {
        // Define o esquema de autenticação padrão como JWT
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x => // Configuração middleware de autenticação JWT Bearer
    {
        x.RequireHttpsMetadata = false; // Não requer HTTPS
        x.SaveToken = true; // Salva o token recebido
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, // Valida a chave de assinatura do emissor
            IssuerSigningKey = new SymmetricSecurityKey(key), // Chave de segurança
            ValidateIssuer = false, // Não valida o emissor
            ValidateAudience = false // Não valida o público-alvo
        };
    });

string? mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(mysqlConnection))
{
    throw new Exception("Erro: a string de conexão 'DefaultConnection' não foi carregada corretamente. Verifique o appsettings.json.");
}

builder.Services.AddDbContext<APIGuiaDBContext>(options =>
                                                    options.UseMySql(mysqlConnection,
                                                    ServerVersion.AutoDetect(mysqlConnection)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
