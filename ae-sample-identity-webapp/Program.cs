using Ae.Sample.Identity.Data;
using Ae.Sample.Identity.Extensions;
using Ae.Sample.Identity.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add WebApp Authentication
builder.Services.AddWebAppAuthentication(ConstsWebApp.CookieName, cookieOptions =>
{
    cookieOptions.Cookie.Name = ConstsWebApp.CookieName;
    cookieOptions.LoginPath = "/Account/Login";
    cookieOptions.AccessDeniedPath = "/Account/AccessDenied";
    cookieOptions.ExpireTimeSpan = TimeSpan.FromSeconds(ConstsWebApp.CookieExpireTimeSeconds);
});

builder.Services.AddTransient<IAppIdentityService, AppIdentityService>();

// Add WebApp Policy etc.
builder.Services.AddWebAppAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
