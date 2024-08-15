using Identity.API.Application;
using Identity.API.Core.Interfaces;
using Identity.API.Core.Interfaces.UnitOfWork;
using Identity.API.Endpoints.Mapster;
using Identity.API.gRPCServices;
using Identity.API.Infrastructure;
using Identity.API.Infrastructure.Repositories;
using Identity.API.Infrastructure.Repositories.UnitOfWork;
using Identity.API.Interceptors;
using Identity.API.Middlewares;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

namespace Identity.API
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

                serverOptions.ConfigureEndpointDefaults(lo =>
                {
                    lo.Protocols = HttpProtocols.Http1AndHttp2;
                    lo.UseHttps();
                });

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
            builder.Services.AddTransient<AuthenticationService>();

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

            builder.Services.AddMapster();
            MapsterConfig.Configure();

            builder.Services.AddHttpClient();

            builder.Services.AddGrpc(opt =>
            {
                opt.Interceptors.Add<ExceptionInterceptor>();
            });
            builder.Services.AddGrpcReflection();

            builder.Services.AddOutputCache();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandling>();

            app.UseHttpsRedirection();
            app.UseOutputCache();

            app.UseAuthorization();

            app.UseMiddleware<ProtocolLoggingMiddleware>();
            app.MapControllers();
            app.MapGrpcService<UserGRPCService>();
            app.MapGrpcReflectionService();

            app.Run();
        }
    }
}