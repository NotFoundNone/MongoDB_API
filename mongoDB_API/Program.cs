using mongoDB_API.Controllers;
using mongoDB_API.Models;
using mongoDB_API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PhoneStoreDatabaseSettings>(
    builder.Configuration.GetSection("PhoneStoreDatabase"));

builder.Services.AddSingleton<PhonesService>();
builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<ManufacturersService>();


builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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