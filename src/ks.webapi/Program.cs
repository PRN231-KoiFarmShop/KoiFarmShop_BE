using ks.application.Services;
using ks.application.Services.Interfaces;
using ks.infras;
using ks.webapi;
using ks.webapi.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "koi-shipment",
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                          .AllowAnyMethod().AllowAnyHeader();
                      });
});

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISessionCartService, SessionCartService>();

var app = builder.Build();

ApplyMigrations();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("koi-shipment");
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.Run();

void ApplyMigrations()
{
    using var scope = app!.Services.CreateScope();
    var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (_db.Database.GetPendingMigrations().Count() > 0)
    {
        _db.Database.Migrate();
    }
}
