using ApiInicial.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var conexionLocal = builder.Configuration.GetConnectionString("CatalogoConection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conexionLocal));

builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

/* builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Ignora las referencias circulares y las reemplaza con null
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
 */
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "Angular",
        policy => policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()
    );
});

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("Angular");

app.UseAuthorization();

app.MapControllers();

app.Run();
