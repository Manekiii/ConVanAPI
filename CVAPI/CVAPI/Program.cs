using CVAPI.Models;
using CVAPI.TokenAuthentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITokenManager, TokenManager>();

builder.Services.AddControllers();

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddDbContext<ConvandbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("dbconn")));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
}*/
app.UseCors("MyPolicy");

app.UseSwagger();

app.UseSwaggerUI();
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
