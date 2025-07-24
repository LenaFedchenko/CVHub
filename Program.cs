using CVHub.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Подключаем EF Core (пример с SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=CVHub.db"));

// 2️⃣ Добавляем сервисы MVC-контроллеров
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();

var app = builder.Build();

// 3️⃣ Настраиваем HTTP-конвейер
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

// 4️⃣ Настраиваем маршрутизацию
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
