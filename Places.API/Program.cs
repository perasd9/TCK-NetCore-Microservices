
using Microsoft.EntityFrameworkCore;
using Places.API.Application;
using Places.API.Core.Interfaces;
using Places.API.Core.Interfaces.UnitOfWork;
using Places.API.Infrastructure;
using Places.API.Infrastructure.Repositories;
using Places.API.Infrastructure.Repositories.UnitOfWork;

namespace Places.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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