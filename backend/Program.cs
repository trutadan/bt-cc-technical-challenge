using Microsoft.EntityFrameworkCore;
using technical_challenge.DatabaseContext;
using technical_challenge.Repository;
using technical_challenge.Repository.Interface;
using technical_challenge.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDatabaseContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection"), npgsqlOptions =>
            npgsqlOptions.SetPostgresVersion(new Version(15, 3))
        )
    );

// Register repositories and services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JWTLoginService>();
builder.Services.AddScoped<OTPLoginService>();

// With the following lines
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(options =>
    {
        options.WithOrigins("http://127.0.0.1:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
}

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
