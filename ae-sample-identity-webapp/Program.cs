using Ae.Sample.Identity.Data;
using Ae.Sample.Identity.Extensions;
using Ae.Sample.Identity.Services;
using Microsoft.AspNetCore.HttpOverrides;

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

builder.Services
    .AddTransient<IAppIdentityService, AppIdentityService>()
    .AddSingleton<IAccountsService, AccountsService>();

// Add WebApp Policy etc.
builder.Services.AddWebAppAuthorization();

var app = builder.Build();
app.UsePathBase(new PathString($"{app.Configuration.GetValue<string>("AppBasePath")}"));
app.UseRouting();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
