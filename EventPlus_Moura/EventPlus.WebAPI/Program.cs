
using EventPlus.WebAPI.BdContextEvento;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

namespace EventPlus.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Configurar o Contexto do Banco de Dados
             builder.Services.AddDbContext<EventoContext>(options => options.UseSqlServer
              (builder.Configuration.GetConnectionString("DefaultConnection")));

            // 2. Registrar as repositories (Injeção de Dependência)
            builder.Services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();
            //2.1 Add TipoUsuario
            builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();
            //2.1 Add Instituicao
            builder.Services.AddScoped<IInstituicaoRepository, InstituicaoRepository>();

            // Adiciona o Swagger
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Api de Eventos",
                Description = "Aplicação para gerenciamento de eventos",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Arthurbr-YT",
                    Url = new Uri("https://www.youtube.com/@Arthurbr-YT"),
                },
                License = new OpenApiLicense
                {
                    Name = "Licença de Uso",
                    Url = new Uri("https://example.com/license"),

                }

            });

            // Usando a autenticação no Swagger
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Insira o token JWT:"

            });

            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<String>().ToList()                
                
                });

            });

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger(options => { });
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty; // Define a raiz para acessar o Swagger UI
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}