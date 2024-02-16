var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();

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
