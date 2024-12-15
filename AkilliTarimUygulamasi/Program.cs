using AkilliTarimUygulamasi.Controllers;
using AkilliTarimUygulamasi.Models;
using AkilliTarimUygulamasi.Services;
using Microsoft.EntityFrameworkCore;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc();

// Veritabanı servisi ve generic servisler
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IFarmDataService, FarmDataService>(); // Scoped olarak ekleyin
builder.Services.AddScoped<FarmController>(); // Eğer Controller'ı manuel eklemeniz gerekiyorsa

builder.Services.AddSoapCore(); // SOAP servisi için SoapCore

var app = builder.Build();


app.UseRouting();  // Yönlendirme işlemi

app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<FarmReportService>("/FarmReportService.asmx", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
});

// Diğer yapılandırmalar
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

app.Run();