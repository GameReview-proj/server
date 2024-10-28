using GameReview.Builders.Impl;
using GameReview.Data;
using GameReview.Models;
using GameReview.Repositories.Impl;
using GameReview.Services.Exceptions;
using GameReview.Services.Impl;
using GameReview.Services.Impl.IGDB;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowCredentials();
    });
});

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

// SERVICES
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BlobService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<CommentaryService>();
builder.Services.AddScoped<FollowService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IGDBService>();
builder.Services.AddSingleton<IGDBTokenService>();

// BUILDERS
builder.Services.AddScoped<UserBuilder>();
builder.Services.AddScoped<ReviewBuilder>();
builder.Services.AddScoped<FollowBuilder>();
builder.Services.AddScoped<CommentaryBuilder>();

// REPOSITORIES
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ReviewRepository>();
builder.Services.AddScoped<FollowRepository>();
builder.Services.AddScoped<CommentaryRepository>();

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

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.UseCors();

app.Run();
