
using Microsoft.EntityFrameworkCore;
using SportingEvents.API.Application;
using SportingEvents.API.Core.Interfaces;
using SportingEvents.API.Core.Interfaces.UnitOfWork;
using SportingEvents.API.Infrastructure;
using SportingEvents.API.Infrastructure.Repositories;
using SportingEvents.API.Infrastructure.Repositories.UnitOfWork;

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