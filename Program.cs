using FilterDemo.Filters;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// if your filter depends on a service
builder.Services.AddScoped<CustomResourceFilter>();
builder.Services.AddScoped<CustomResultFilter>();

builder.Services.AddControllersWithViews(options =>
{
  //options.Filters.Add(new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter());
  options.Filters.Add<CustomExceptionFilter>();
  options.Filters.Add<CustomActionFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
