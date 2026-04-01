using Microsoft.EntityFrameworkCore;
using TarefasAPI.BdContextTarefas;
using TarefasAPI.Interfaces;
using TarefasAPI.Repositories;


namespace TarefasAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<ITarefasRepositories, TarefasRepositories>();

            builder.Services.AddDbContext<TarefasContext>(options => options.UseSqlServer
        (builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
                {
                    Version = "v1",
                    Title = "API de Eventos",
                    Description = "Aplicacao para gerenciamento de eventos",
                    TermsOfService = new Uri("https://youtu.be/Tk9mndIdsJo"),
                    Contact = new Microsoft.OpenApi.OpenApiContact
                    {
                        Name = "Cauhê Matheus",
                        Url = new Uri("https://github.com/CauheM")
                    },
                    License = new Microsoft.OpenApi.OpenApiLicense
                    {
                        Name = "Licensa",
                        Url = new Uri("https://rickroll.it/rickroll.mp4")
                    }
                });


                // Add services to the container.
                builder.Services.AddRazorPages();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseAuthorization();

                app.MapStaticAssets();
                app.MapRazorPages()
                   .WithStaticAssets();

                app.Run();
            }
        }
    }
}
