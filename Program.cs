using Microsoft.EntityFrameworkCore;
/*
ASP.NET Core is built with dependency injection (DI).
Services, such as the database context (below), are registered with DI in Program.cs.
These services are provided to components that require them via constructor parameters (e.g., in MoviesController.cs) ...
*/
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
// Add services to the container. builder.Services.AddControllers(); builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); builder.Services.AddEndpointsApiExplorer(); builder.Services.AddSwaggerGen();
// var app = builder.Build();

/*
The following highlighted code in Program.cs shows how to use SQLite in development and SQL Server in production.
... [From NOTES in MovieController.cs:] ... Scaffolding generated the following highlighted code in Program.cs:
The ASP.NET Core configuration system reads the "MvcMovieContext" database connection string. (Defined in appsettings.json)
*/

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<CanuckContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("CanuckContext") ?? throw new InvalidOperationException("Connection string 'CanuckContext' not found.")));
}
else
{
    builder.Services.AddDbContext<CanuckContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("CanuckContext") ?? throw new InvalidOperationException("Connection string 'CanuckContext' not found.")));
} // end if builder.Environment.IsDevelopment

/*
// Scaffolding generated the following highlighted code in Program.cs:
// NOTE: the commented-out lines were originally created in the scaffolding
// the versions of this above, was in the tutorial for Sqlite vs. MSSQL purposes ...
builder.Services.AddDbContext<CanuckContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CanuckContext") ?? throw new InvalidOperationException("Connection string 'CanuckContext' not found.")));
*/
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

// Define a CORS Policy:
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Replace with your Angular app's origin
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

/*
Configure Middleware for Swagger: Make sure Swagger middleware is included in the pipeline.
Replace the existing Swagger configuration with this:
*/
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

// Use the CORS Policy in Middleware:
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
