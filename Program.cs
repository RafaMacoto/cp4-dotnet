using cp4_dotnet.Middlewares;
using cp4_dotnet.Models.Factory;
using cp4_dotnet.Models.Interfaces;
using cp4_dotnet.Services;
using System.Reflection;

namespace cp4_dotnet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "GeoMaster API",
                    Version = "v1",
                    Description = "API de cálculos geométricos (CP4)."
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                }
            });

            // DI
            builder.Services.AddSingleton<ICalculadoraService, CalculadoraService>();
            builder.Services.AddSingleton<IFormaFactory, FormaFactory>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeoMaster API v1");
                    c.RoutePrefix = string.Empty; // Swagger na raiz
                });
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
