using JobPortal.API.Services;
using Microsoft.EntityFrameworkCore;
using TalentBridgePortal.Data;
using TalentBridgePortal.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Register HttpClient (REQUIRED)
builder.Services.AddHttpClient();

// Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IResumeParserService, ResumeParserService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// ✅ ADD CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// ✅ USE CORS BEFORE AUTH
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();