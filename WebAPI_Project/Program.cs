using Microsoft.EntityFrameworkCore;
using WebAPI_Project.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PetDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaulConnections")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<PetDbContext>();
        PetData.Init(context);
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {

    }
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
