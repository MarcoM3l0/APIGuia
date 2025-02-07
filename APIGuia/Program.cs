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

// Convertendo a chave de seguran�a em um array de bytes para ser usada na gera��o do token JWT
var key = Encoding.ASCII.GetBytes(Settings.secret);

// Configura��o do JWT
builder.Services.AddAuthentication(x =>
    {
        // Define o esquema de autentica��o padr�o como JWT
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x => // Configura��o middleware de autentica��o JWT Bearer
    {
        x.RequireHttpsMetadata = false; // N�o requer HTTPS
        x.SaveToken = true; // Salva o token recebido
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, // Valida a chave de assinatura do emissor
            IssuerSigningKey = new SymmetricSecurityKey(key), // Chave de seguran�a
            ValidateIssuer = false, // N�o valida o emissor
            ValidateAudience = false // N�o valida o p�blico-alvo
        };
    });

string? mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(mysqlConnection))
{
    throw new Exception("Erro: a string de conex�o 'DefaultConnection' n�o foi carregada corretamente. Verifique o appsettings.json.");
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
