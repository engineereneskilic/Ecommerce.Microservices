using FreeCourse.Services.Catalog.Services;
using FreeCourse.Services.Catalog.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Scopes
builder.Services.AddScoped<ICategoryService, CategoryService>();


builder.Services.AddAutoMapper(typeof(Program));


//DB
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

var settings = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
if (settings is null)
{
    throw new InvalidOperationException("DatabaseSettings section is missing or misconfigured.");
}
// DB last

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
