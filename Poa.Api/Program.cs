using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Poa.Api.Endpoints;
using Poa.Api.Middleware;
using Proy.Infrastructure.IoC.Di;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        b => b
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed((hosts) => true));
});

// 🔹 REGISTRO DE SERVICIOS PERSONALIZADOS
builder.Services
    .RegisterDataBase(builder.Configuration)
    .RegisterRepositories()
    .RegisterServices()
    .RegisterProviders()
    .RegisterLibraries();

// 🔹 CONFIGURACIÓN DE AUTENTICACIÓN JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
// builder.Services.AddAuthorization(options =>
// {
//     options.FallbackPolicy = options.DefaultPolicy; // 🔹 Exige autorización en todos los endpoints
// });
//
// app.Use(async (context, next) =>
// {
//     var path = context.Request.Path;
//     if (path.StartsWithSegments("/api/auth/login"))
//     {
//         await next(); // Permitir acceso sin autenticación
//         return;
//     }
//
//     if (context.User.Identity?.IsAuthenticated == false)
//     {
//         context.Response.StatusCode = StatusCodes.Status401Unauthorized;
//         await context.Response.WriteAsync("Unauthorized");
//         return;
//     }
//
//     await next();
// });
app.UseCors("CORSPolicy");
app.UseMiddleware<MiddlewareException>();
app.UseMiddleware<NotFoundMiddleware>();
app.UseMiddleware<AuthorizationMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIS SUINT-ACADEMIC V.1.0");
    c.RoutePrefix = "swagger";
    c.EnableFilter();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔹 REGISTRO DE ENDPOINTS
app.MapUserEndpoints();
app.MapAuthEndpoints();

app.Run();
