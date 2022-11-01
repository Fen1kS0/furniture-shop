using FluentValidation.AspNetCore;
using FurnitureShop.Backend.BL;
using FurnitureShop.Backend.DataAccess;
using FurnitureShop.Backend.Report;
using FurnitureShop.Backend.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization(opt => opt.ResourcesPath = builder.Configuration["ResourcesPath"]);

builder.Services.AddCustomController();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataContext(builder.Configuration);

builder.Services.AddServices();
builder.Services.AddMappings();
builder.Services.AddValidators();
builder.Services.AddReportService();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

var app = builder.Build();

#if DEBUG
//await new DataContext().Database.MigrateAsync();
#endif

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(configurePolicy => configurePolicy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapControllers();

await app.RunAsync();