using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Proy.Infrastructure.DataBase.EntityFramework.Context;

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
        collection.AddValidatorsFromAssembly(Assembly.Load("Academic.Application"));
        ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) => memberInfo?.Name;
        return collection;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection collection)
    {
        return collection;
    }
    
    public static IServiceCollection RegisterRepositories(this IServiceCollection collection)
    {
        return collection;
    }
}