using Microsoft.EntityFrameworkCore;
using Serilog;
using WorldCountry.API.Common;
using WorldCountry.API.Data;
using WorldCountry.API.Repository;
using WorldCountry.API.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Configure CORS

 builder.Services.AddCors(option =>
{
    option.AddPolicy("CustomPolicy", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

#endregion


#region Configure Database

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
#endregion

#region AutoMapper

builder.Services.AddAutoMapper(typeof(MappingProfile));

#endregion

#region Repository

builder.Services.AddTransient<ICountryRepository, CountryRepository>();
builder.Services.AddTransient<IStateRepository, StateRepository>();

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

#endregion

#region config Serilog

builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.File("Logs/Log.txt",rollingInterval:RollingInterval.Day);

    if(context.HostingEnvironment.IsProduction() == false)
    {
        config.WriteTo.Console();
    }
}
);

#endregion





builder.Services.AddControllers();
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

app.UseCors("CustomPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
