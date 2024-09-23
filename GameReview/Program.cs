using GameReview.Data;
using GameReview.Models;
using GameReview.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlServerConnectionString = builder.Configuration["ConnectionStrings:GameReview"];

builder.Services.AddDbContext<DatabaseContext>(opts =>
{
    opts
        .UseSqlServer(sqlServerConnectionString)
        .UseLazyLoadingProxies();
});

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<TokenService>();

builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
