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

builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

ApplyMigrations();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("koi-shipment");
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

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
