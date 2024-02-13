using GW_AdminConfig.Repository.Interface;
using GW_AdminConfig.Repository.Service;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IPathConfigSettings, PathConfigSettings>();
builder.Services.AddScoped<ILaneDevices, LaneDevices>();

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions = new DefaultContractResolver());
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
    pattern: "{controller=AdminHome}/{action=Index}/{id?}");

app.Run();


