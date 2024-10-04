using GameReview.Data;
using GameReview.Data.DTOs.Commentary;
using GameReview.Models;
using GameReview.Services;
using GameReview.Services.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add(new GlobalExceptionFilter());
});

var sqlServerConnectionString = builder.Configuration["ConnectionStrings:GameReview"];
builder.Services.AddDbContext<DatabaseContext>(opts =>
{
    opts
        .UseSqlServer(sqlServerConnectionString)
        .UseLazyLoadingProxies();
});

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<ICommentaryService, CommentaryService>();
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
