using Microsoft.OpenApi.Models;
using qr_factorizacion_api_cs.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Clave secreta para firmar el token (deber�a estar en appsettings.json o variable de entorno)
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// SWAGGER PARA PROBAR LOCAL
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "QR Factorization API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// REGISTRA LOS CONTROLADORES
builder.Services.AddControllers();
builder.Services.AddScoped<qr_factorizacion_api_cs.Services.QrFactorizationService>();
builder.Services.AddScoped<qr_factorizacion_api_cs.Services.ExternalApiService>();
builder.Services.Configure<ExternalApiOptions>(builder.Configuration.GetSection("ExternalApi"));
builder.Services.AddHttpClient();


var app = builder.Build();

// SWAGGER PARA PROBAR LOCAL
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "QR Factorization API v1");
});
app.UseAuthentication();
app.UseAuthorization();

// MAPEA LOS CONTROLADORES
app.MapControllers();
app.UseCors("AllowAll");
app.Run();