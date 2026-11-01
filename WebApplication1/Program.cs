using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Contracts;
using WebApplication1.Repository;
using WebApplication1.Services;
using WebApplication1.Validations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<MovieService>();
builder.Services.AddScoped<IValidator<UserRegisterContract>, RegisterRequestValidator>();
builder.Services.AddScoped<IValidator<UserLoginContract>, LoginRequestValidator>();
builder.Services.AddScoped<IValidator<MovieRequest>, MovieValidator>();

builder.Services.AddCors(options =>
    options.AddPolicy("ReactClient", policy =>
        policy.WithOrigins(builder.Configuration.GetSection("ClientOrigins").Get<string[]>() ?? ["http://localhost:5173"])
              .AllowAnyHeader()
              .AllowAnyMethod()));

var app = builder.Build();
app.MapOpenApi();
app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "EFDiExample"));
app.UseHttpsRedirection();
app.UseCors("ReactClient");
app.UseAuthorization();
app.MapControllers();

app.Run();