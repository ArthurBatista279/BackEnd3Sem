using ConnectPlus_Moura.BdContextEvento;
using ConnectPlus_Moura.Interfaces;
using ConnectPlus_Moura.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

namespace ConnectPlus_Moura;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ConnectPlus API",
                Description = "API ConnectPlus_Moura",
                Contact = new OpenApiContact
                {
                    Name = "Arthur Batista",
                    Url = new Uri("https://github.com/ArthurBatista279")
                }
            });

            var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
        });

        builder.Services.AddRazorPages();

        builder.Services.AddDbContext<EventoContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        builder.Services.AddScoped<ITipoContatoRepository, TipoContatoRepository>();
        builder.Services.AddScoped<IContatoRepository, ContatoRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConnectPlus API v1");
            c.RoutePrefix = string.Empty;
        });

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();
        
        app.MapControllers();
        
        app.MapStaticAssets();
        app.MapRazorPages()
           .WithStaticAssets();

        app.Run();
    }
}
