
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Users.API.Application;
using Users.API.Core.Interfaces;
using Users.API.Core.Interfaces.UnitOfWork;
using Users.API.Infrastructure;
using Users.API.Infrastructure.Repositories;
using Users.API.Infrastructure.Repositories.UnitOfWork;

namespace Users.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                //serverOptions.Limits.MaxConcurrentConnections = 5000;
                //serverOptions.Limits.MaxConcurrentUpgradedConnections = 5000;

                //ThreadPool.SetMinThreads(200, 200);

                serverOptions.ConfigureEndpointDefaults(lo => lo.Protocols = HttpProtocols.Http1AndHttp2);

            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<UsersContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<UserService>();

            builder.Services.AddHttpClient();
            
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ProtocolLoggingMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
    public class ProtocolLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProtocolLoggingMiddleware> _logger;

        public ProtocolLoggingMiddleware(RequestDelegate next, ILogger<ProtocolLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogWarning($"Protocol: {context.Request.Protocol}");

            await _next(context);
        }
    }
}