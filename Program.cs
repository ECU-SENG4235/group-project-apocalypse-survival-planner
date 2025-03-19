using ASP.Components;
using ASP.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<PlanService>();

var userDbConnectionString = builder.Configuration.GetConnectionString("ApplicationDbContext")
    ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.");

var plansDbConnectionString = builder.Configuration.GetConnectionString("PlansDbContext")
    ?? throw new InvalidOperationException("Connection string 'PlansDbContext' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(userDbConnectionString));

builder.Services.AddDbContext<PlansDbContext>(options =>
    options.UseSqlite(plansDbConnectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddFluentUIComponents();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        if (!(context.User.Identity?.IsAuthenticated ?? false))
        {
            context.Response.Redirect("/Identity/Account/Register");
            return;
        }
        else
        {
            context.Response.Redirect("/Home");
            return;
        }
    }
    await next();
});

app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
