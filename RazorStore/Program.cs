using Microsoft.EntityFrameworkCore;
using RazorStore.Model;
using RazorStore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authorization;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddGoogle(googleOption =>
{
    googleOption.ClientId = builder.Configuration["Google:ClientId"];
    googleOption.ClientSecret = builder.Configuration["Google:ClientSecret"];
});
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("CanManageGoods", policyBuilder =>
    policyBuilder.AddRequirements(new IsGoodsDeleteRequirement()));
});
builder.Services.AddScoped<IAuthorizationHandler, IsGoodDeleteHandler>();
// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

