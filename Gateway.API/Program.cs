
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Gateway.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapReverseProxy();
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