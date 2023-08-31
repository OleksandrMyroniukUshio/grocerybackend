using groceries_api.Database;
using groceries_api.Services;
using Microsoft.EntityFrameworkCore;

 
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5191");
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<GroceriesDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IGroceriesListService, GroceriesListService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); //change later

app.UseAuthorization();

app.MapControllers();

app.Run();
