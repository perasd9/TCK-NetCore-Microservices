
using Microsoft.EntityFrameworkCore;
using TypesOfSportingEvents.API.Application;
using TypesOfSportingEvents.API.Core.Interfaces;
using TypesOfSportingEvents.API.Core.Interfaces.UnitOfWork;
using TypesOfSportingEvents.API.Infrastructure;
using TypesOfSportingEvents.API.Infrastructure.Repositories;
using TypesOfSportingEvents.API.Infrastructure.Repositories.UnitOfWork;

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

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<ITypeOfSportingEventRepository, TypeOfSportingEventRepository>();
            builder.Services.AddTransient<TypeOfSportingEventService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}