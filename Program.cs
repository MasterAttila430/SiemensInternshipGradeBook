using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Registering the repository with the new interface name
builder.Services.AddScoped<IItemRepository, ItemRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
