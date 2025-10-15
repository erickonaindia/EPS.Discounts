using EPS.Discounts.API;
using EPS.Discounts.API.Hubs;
using EPS.Discounts.API.Services;
using EPS.Discounts.Application.Interfaces;
using EPS.Discounts.Application.Services;
using EPS.Discounts.Infrastructure;
using EPS.Discounts.Infrastructure.Repositories;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Kestrel: HTTP/1.1 + HTTP/2 on :8080
builder.WebHost.ConfigureKestrel(k =>
{
    k.ListenAnyIP(8080, o => o.Protocols = HttpProtocols.Http1AndHttp2);
});

// SignalR for progress notifications TODO
//builder.Services.AddSignalR();

builder.Services.AddGrpc(options => { options.EnableDetailedErrors = true; });

// Progress notifications (DI)
builder.Services.AddSingleton<IProgressNotifier, ProgressNotifier>();

builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p =>
        p.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod());
});

// Static files and basic routing
builder.Services.AddRouting();
builder.Services.AddDirectoryBrowser();

// PostgreSQL (EF Core)
var cs = builder.Configuration.GetConnectionString("Postgres");
if (string.IsNullOrWhiteSpace(cs))
{
    throw new InvalidOperationException("Missing connection string 'Postgres'.");
}

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<DiscountDbContext>(opt =>
    opt.UseNpgsql(
        cs!,
        npg => npg.MigrationsAssembly(typeof(DiscountDbContext).Assembly.FullName)
        )
    );

builder.Services.AddScoped<IDiscountRepository, EfDiscountRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDiscountService, DiscountService>();

var app = builder.Build();

// Automatic migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();
    await db.Database.MigrateAsync();
}

app.UseCors();
app.UseGrpcWeb();

// Endpoints
app.MapGrpcService<DiscountGrpcService>().EnableGrpcWeb();
app.MapHub<ProgressHub>("/hubs/progress");

// Static files (wwwroot) and default files
app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();
