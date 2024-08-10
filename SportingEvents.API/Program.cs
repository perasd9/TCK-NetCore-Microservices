using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SportingEvents.API.Application;
using SportingEvents.API.Core.Interfaces;
using SportingEvents.API.Core.Interfaces.UnitOfWork;
using SportingEvents.API.Endpoints.Mapster;
using SportingEvents.API.gRPCServices;
using SportingEvents.API.Infrastructure;
using SportingEvents.API.Infrastructure.Repositories;
using SportingEvents.API.Infrastructure.Repositories.UnitOfWork;
using SportingEvents.API.Interceptors;
using System.Text;

namespace SportingEvents.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<SportingEventsContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<ISportingEventRepository, SportingEventRepository>();
            builder.Services.AddTransient<SportingEventService>();

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

            builder.Services.AddGrpc(opt =>
            {
                opt.Interceptors.Add<ExceptionInterceptor>();
            });
            builder.Services.AddGrpcReflection();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandling>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGrpcReflectionService();

            app.MapControllers();
            app.MapGrpcService<SportingEventGRPCService>();

            app.Run();
        }
    }
}