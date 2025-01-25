using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//AddScoped = 作用域=生命週期與HTTP請求一樣長
//Transient = 作用域=方法級別=此方法結束後清除 不適合用於儲存庫=太早結束=每次使用不同儲存庫都要重新創建
//Singleton = 作用域=整個應用程式直到應用程式結束
builder.Services.AddScoped<INewsRepository,NewRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();
