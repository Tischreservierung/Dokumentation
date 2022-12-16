using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tischreservierung.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OnlineReservationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TischreservierungContext") ?? throw new InvalidOperationException("Connection string 'TischreservierungContext' not found.")));

// Add services to the container.
//Services.AddScopped
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IRestaurantTableRepository, RestaurantTableRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
