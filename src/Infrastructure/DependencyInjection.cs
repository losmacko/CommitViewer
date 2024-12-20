﻿using CommitViewer.Application.Common.Interfaces;
using CommitViewer.Infrastructure.Configuration;
using CommitViewer.Infrastructure.Data;
using CommitViewer.Infrastructure.VersionControlSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IGIT, GIT>();
        services.AddSingleton(TimeProvider.System);

        services.Configure<GitOptions>(configuration.GetSection(GitOptions.Name));
        services.AddHttpClient();

        return services;
    }
}
