using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NewStarterProject.NewStarter.Domain.Interfaces;
using NewStarterProject.NewStarter.Domain.Services;
using NewStarterProject.NewStarter.Infrastructure.Datastore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var connectionString = Environment.GetEnvironmentVariable("ConnectionString", EnvironmentVariableTarget.Process);

builder.Services.AddDbContext<IDataStore, StarterProjectContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IDoorService, DoorService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccessControlService, AccessControlService>();

builder.Services.AddControllers();

#if DEBUG

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
});

builder.Services.AddOpenApiDocument();

#endif

var app = builder.Build();

#if DEBUG

app.UseOpenApi();
app.UseSwaggerUi();

#endif

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();