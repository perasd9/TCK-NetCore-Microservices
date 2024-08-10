using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Reservations.API.Core.Interfaces;
using Reservations.API.Core.Interfaces.UnitOfWork;
using Reservations.API.Endpoints.Mapster;
using Reservations.API.gRPCServices;
using Reservations.API.Infrastructure;
using Reservations.API.Infrastructure.Repositories;
using Reservations.API.Infrastructure.Repositories.UnitOfWork;
using Reservations.API.Interceptors;
using Reservations.API.Middlewares;
using System.Text;
using Users.API.Application;

namespace Reservations.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ReservationsContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IReservationRepository, ReservationRepository>();
            builder.Services.AddTransient<ReservationService>();

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
            app.MapGrpcService<ReservationGRPCService>();

            app.Run();
        }
    }
}