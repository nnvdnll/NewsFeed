using MVS.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MyApplicationContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    // указывает в какой файл будет обрабатываться запрос, если не указан контроллер и действие
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
