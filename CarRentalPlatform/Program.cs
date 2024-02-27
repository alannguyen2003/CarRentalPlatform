using CarRentalPlatform.Configuration;
using DataAccess.DataAccessLayer;
using Microsoft.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession(options =>
{

});
builder.Services.AddDependence(builder.Configuration);
builder.Services.AddMvcCore();
builder.Services.AddScoped<CarEntityDAO>();
builder.Services.AddSession();

builder.Services.AddScoped<CarEntityDAO>();
builder.Services.AddScoped<AccountDao>();
builder.Services.AddScoped<BrandDAO>();
builder.Services.AddScoped<LocationDAO>();

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

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();