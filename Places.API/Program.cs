
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Places.API.Application;
using Places.API.Core.Interfaces;
using Places.API.Core.Interfaces.UnitOfWork;
using Places.API.Endpoints;
using Places.API.gRPCServices;
using Places.API.Infrastructure;
using Places.API.Infrastructure.Repositories;
using Places.API.Infrastructure.Repositories.UnitOfWork;
using System.Text;

namespace Places.API
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
            builder.Services.AddDbContext<PlacesContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IPlaceRepository, PlaceRepository>();
            builder.Services.AddTransient<PlaceService>();

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JWT")["Key"] ?? ""))
                };
            });

            builder.Services.AddAuthorization();

            builder.Services.AddGrpc();
            builder.Services.AddGrpcReflection();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGrpcReflectionService();

            app.MapControllers();
            app.MapGrpcService<PlaceGRPCService>();

            app.UseMiddleware<ProtocolLoggingMiddleware>();

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