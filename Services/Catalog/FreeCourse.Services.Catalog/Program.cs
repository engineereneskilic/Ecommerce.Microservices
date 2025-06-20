using FreeCourse.Services.Catalog.Services;
using FreeCourse.Services.Catalog.Settings;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["IdentityServerURL"];
        options.RequireHttpsMetadata = false; // Geliþtirme ortamý için
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CatalogScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "catalog_fullpermission");
    });
});

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Catalog API",
        Version = "v1"
    });
});

//Scopes
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddAutoMapper(typeof(Program));

//DB
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1");
        c.RoutePrefix = string.Empty;  // Swagger ana sayfa olarak açýlýr
    });
}

AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
{
    Console.WriteLine("UNHANDLED EXCEPTION:");
    Console.WriteLine(args.ExceptionObject.ToString());
};

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
