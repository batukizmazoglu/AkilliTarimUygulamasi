using Microsoft.EntityFrameworkCore;
using AkilliTarimUygulamasi.Models;
using AkilliTarimUygulamasi.Services;
using AkilliTarimUygulaması.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Generic services
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

// API anahtarını appsettings.json'dan al
string apiKey = builder.Configuration["AccuWeather:ApiKey"];

// WeatherService'i DI konteynerine ekle
builder.Services.AddHttpClient<WeatherService>(client =>
{
    client.BaseAddress = new Uri("http://dataservice.accuweather.com/");
});

// WeatherService'i API anahtarıyla yapılandırmak için bağımlılıkları çöz
builder.Services.AddSingleton(provider =>
{
    var httpClient = provider.GetRequiredService<HttpClient>();
    return new WeatherService(httpClient, apiKey);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();