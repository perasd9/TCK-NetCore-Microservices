
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TypesOfSportingEvents.API.Application;
using TypesOfSportingEvents.API.Core.Interfaces;
using TypesOfSportingEvents.API.Core.Interfaces.UnitOfWork;
using TypesOfSportingEvents.API.gRPCServices;
using TypesOfSportingEvents.API.Infrastructure;
using TypesOfSportingEvents.API.Infrastructure.CachingRepository;
using TypesOfSportingEvents.API.Infrastructure.Repositories;
using TypesOfSportingEvents.API.Infrastructure.Repositories.UnitOfWork;
using TypesOfSportingEvents.API.Interceptors;
using TypesOfSportingEvents.API.Middlewares;

namespace TypesOfSportingEvents.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<TypesOfSportingEventsContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddMemoryCache();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<TypeOfSportingEventRepository>();
            builder.Services.AddScoped<ITypeOfSportingEventRepository>(sp =>
            {
                var typeRepository = sp.GetRequiredService<TypeOfSportingEventRepository>();

                var cache = sp.GetRequiredService<IMemoryCache>();

                return new CachingTypeOfSportingEventRepository(cache, typeRepository);
            });

            builder.Services.AddTransient<TypeOfSportingEventService>();

            builder.Services.AddGrpc(opt =>
            {
                opt.Interceptors.Add<ExceptionInterceptor>();
            });

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
            app.MapGrpcService<TypeOfSportingEventGRPCService>();

            app.Run();
        }
    }
}