using CursVN.API.DI;
using CursVN.Application.Other;
using CursVN.Core.Abstractions.Other;
using CursVN.Persistance;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddDataServices();
builder.Services.AddAuthServices();
builder.Services.AddScoped<IImageService, ImgBBService>(x => new ImgBBService(builder.Configuration["ApiKey"].ToString()));
builder.Services.AddScoped<IEmailService, EmailService>(x => new EmailService(
        sender: builder.Configuration.GetSection("Email")["Address"],
        password: builder.Configuration.GetSection("Email")["Password"]
    ));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(o =>
{
    o.AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod();
});

app.MapControllers();

app.Run();
