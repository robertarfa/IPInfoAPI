using IPInfoAPI.BackgroundServices;
using IPInfoAPI.Data;
using IPInfoAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IP2CService>();
builder.Services.AddScoped<IPInfoService>();
builder.Services.AddSingleton<CacheService>();
builder.Services.AddHostedService<IPUpdateService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();