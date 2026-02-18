using Microsoft.EntityFrameworkCore;
using FilmesMoura.WebAPI.Interfaces;
using FilmesMoura.WebAPI.BdContextFilme;
using FilmesMoura.WebAPI.Repository;



var builder = WebApplication.CreateBuilder(args);


//Adiciona o contexto do banco de dados (SQL Server)
builder.Services.AddDbContext<FilmeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();
