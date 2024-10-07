using data_access;
using data_access.interfaces;
using data_access.repositories;
using Microsoft.EntityFrameworkCore;
using service.interfaces;
using service.services;

namespace api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<MyDbContext>(options =>
        {
            options.UseNpgsql(Environment.GetEnvironmentVariable("conn"));
            options.EnableSensitiveDataLogging();
        });

        builder.Services.AddControllers();

        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<IPaperPropertyRepository, PaperPropertyRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IOrderEntryRepository, OrderEntryRepository>();
        builder.Services.AddScoped<IPaperRepository, PaperRepository>();

        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<IPaperPropertyService, PaperPropertyService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IPaperService, PaperService>();

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}