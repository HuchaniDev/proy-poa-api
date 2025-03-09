using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Proy.Application.Services.Authentication;
using Proy.Application.Services.Security;
using Proy.Domain.JwtService;
using Proy.Domain.Models.Authentication;
using Proy.Domain.Repositories.Authentication;
using Proy.Infrastructure.DataBase.EntityFramework.Context;
using Proy.Infrastructure.DataBase.EntityFramework.Repositories.Authentication;
using Proy.Infrastructure.JwtService;
using AuthService = Proy.Infrastructure.JwtService.AuthService;

namespace Proy.Infrastructure.IoC.Di;

public static class ProjectDi
{
    public static IServiceCollection RegisterDataBase(this IServiceCollection collection, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("LocalConnection");

        collection.AddDbContext<ProyDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        return collection;
    }
    
    public static IServiceCollection RegisterLibraries(this IServiceCollection collection)
    {
        collection.AddValidatorsFromAssembly(Assembly.Load("Proy.Application"));
        ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) => memberInfo?.Name;
        return collection;
    }

    public static IServiceCollection RegisterProviders(this IServiceCollection collection)
    {
        //collection.AddScoped<IPasswordHasher<UserModel>, PasswordHasher<UserModel>>();
        collection.AddSingleton<PasswordHasherService>();
        collection.AddScoped<ITokenService,TokenService>();
        // collection.AddScoped<IAuthService, AuthService>();
        return collection;
    }
    
    public static IServiceCollection RegisterServices(this IServiceCollection collection)
    {
        collection.AddTransient<UserService>();
        collection.AddScoped<AuthService>();
        return collection;
    }
    
    public static IServiceCollection RegisterRepositories(this IServiceCollection collection)
    {
        collection.AddTransient<IUserRepository, UserRepository>();
        return collection;
    }
}