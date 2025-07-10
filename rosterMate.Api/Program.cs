using Microsoft.EntityFrameworkCore;
using RossteraMate.Application.Services;
using RosterMate.Application.Interfaces;
using RosterMate.Domain.Interfaces;
using RosterMate.Infrastructure.Data;
using RosterMate.Infrastructure.Repositories;
using AutoMapper;
using RosterMate.Application.Mappings;
using RosterMate.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<RosterMate.Infrastructure.Data.ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    //regiser services
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Register application services (Dependency Injection)
    builder.Services.AddScoped<IStaffRepository, StaffRepository>();
    builder.Services.AddScoped<IStaffService, StaffService>();
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    // Register AutoMapper
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    var app = builder.Build();

    // seed the DB
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await DbSeeder.SeedAsync(dbContext);
    }
    

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapControllers();
    await app.RunAsync();

public partial class Program { }


