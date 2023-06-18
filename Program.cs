using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QLSVAPI.Data;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<QLSVAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QLSVAPIContext") ?? throw new InvalidOperationException("Connection string 'QLSVAPIContext' not found.")));


//fix errors CORS
builder.Services.AddCors(options => options.AddPolicy("MyCors", build =>
               build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//fix errors CORS
app.UseCors("MyCors");


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
