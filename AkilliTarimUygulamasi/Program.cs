using AkilliTarimUygulamasi.Models;
using AkilliTarimUygulamasi.Services;
using Microsoft.EntityFrameworkCore;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Session middleware'i ekleyelim
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register IHttpClientFactory for DI
builder.Services.AddHttpClient();  // Registers HttpClientFactory to use HttpClient in services

// Read the API key from configuration and register WeatherService
builder.Services.AddScoped<WeatherService>(serviceProvider =>
{
    var apiKey = builder.Configuration.GetValue<string>("AccuWeather:ApiKey"); // Get the API key from configuration
    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    return new WeatherService(httpClientFactory, apiKey);
});

// Veritabanı servisi ve generic servisler
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .LogTo(Console.WriteLine, LogLevel.Information)); // Loglama ekleyin

// Generic Service ve diğer servisleri Scoped olarak ekleyin
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<IFarmDataService, FarmDataService>(); // Scoped olarak ekleyin

// SOAP servisi için SoapCore'ı ekleyin
builder.Services.AddSoapCore();

var app = builder.Build();

// Ensure that UseRouting() is called before UseEndpoints().
app.UseRouting();  // This enables routing for controllers and views

// Map the default route (HomeController/Index) for the home page
app.MapDefaultControllerRoute();  // This maps the home page route to Home/Index

// If you're using API controllers, map them here
app.MapControllers();  // This will handle routes like /api/Farm

// Static files (CSS, JS, etc.)
app.UseStaticFiles();  // Ensure static files can be served

app.UseAuthorization();  // Yetkilendirme işlemi

// Session middleware'ini kullan
app.UseSession();

// Hata sayfası geliştirme ortamında gösterilecek
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Geliştirme ortamında hata sayfası
    app.UseSwagger();  // Swagger'ı aktif et
    app.UseSwaggerUI();  // Swagger UI'yi aktif et
}

// SOAP servislerinin kullanılabilmesi için gerekli yapılandırma
app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<FarmReportService>("/FarmReportService.asmx", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
});

// Uygulama başlatılıyor
app.Run();
