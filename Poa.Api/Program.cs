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

builder.Services
    .RegisterDataBase(builder.Configuration)
    .RegisterRepositories()
    .RegisterServices()
    .RegisterLibraries();

var app = builder.Build();


app.UseCors("CORSPolicy");
app.UseMiddleware<MiddlewareException>();
app.UseMiddleware<NotFoundMiddleware>();
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

//add endpoints
app.MapUserEndpoints();

app.Run();
