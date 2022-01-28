using GISSA.Models;
using GISSA.Repositorios;
using GISSA.Services;
using Login.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<UsuariosRepositorio>();
builder.Services.AddScoped<TelefonosRepositorio>();
builder.Services.AddScoped<HabilidadesRepositorio>();
builder.Services.AddAuthorization(options => options.AddPolicy("RoleAdmin", policy => policy.Requirements.Add(new RoleAdmin("A"))));
builder.Services.AddSingleton<IAuthorizationHandler, HandlerRoleAdmin>();
builder.Services.AddAuthentication(options => {

    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(options => {
    options.LoginPath = "/Login"; options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.Redirect("/Login");
        return Task.CompletedTask;
    };
});
//builder.Services.AddScoped<RepositorioCentral>();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
