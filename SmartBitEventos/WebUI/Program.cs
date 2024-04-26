using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddHttpContextAccessor();

//crear usa sesion defecto para la sesion 
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Smartbit-Cookie";
    options.IdleTimeout = TimeSpan.FromSeconds(1360);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = false;
}

);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Equipo}/{action=Equipo}/{id?}");

app.Run();
