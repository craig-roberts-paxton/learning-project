using Microsoft.EntityFrameworkCore;
using NewStarterProject.Model;
using NewStarterProject.Services;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<StarterProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));


#if DEBUG

builder.Services.AddOpenApiDocument();

#endif

var app = builder.Build();

#if DEBUG

    app.UseOpenApi();
    app.UseSwaggerUi();

#endif

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
