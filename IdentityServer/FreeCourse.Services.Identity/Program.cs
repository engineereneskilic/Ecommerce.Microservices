using System.IdentityModel.Tokens.Jwt;
using FreeCourse.Services.Identity.Data;
using FreeCourse.Services.Identity.Services;
using FreeCourse.Services.Identity.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------------------
// 1. Database ve Identity Ayarları
// ------------------------------------------------------------------

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

// ------------------------------------------------------------------
// 2. IdentityServer Ayarları
// ------------------------------------------------------------------

// sub claim'ini bozmamak için IdentityServer'ın varsayılan dönüşümünü iptal et
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddDeveloperSigningCredential()
    .AddResourceOwnerValidator<IdentityResourceOwnerPasswordvalidator>();

// ------------------------------------------------------------------
// 3. Controller ve Swagger Ayarları
// ------------------------------------------------------------------

builder.Services.AddControllers(); // ← Bunu mutlaka eklemelisin

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header. Örnek: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ------------------------------------------------------------------
// 4. Build ve Middleware Pipeline
// ------------------------------------------------------------------

var app = builder.Build();

// Veritabanına default kullanıcı ekle
await IdentitySeed.SeedDefaultUserAsync(app.Services);

// Middleware sırası ÖNEMLİ!
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API V1");
    });
}

app.UseHttpsRedirection();

// IdentityServer middleware’i (Authentication + Authorization içerir)
app.UseIdentityServer();

// Controller endpointlerini aktifleştir
app.MapControllers();

app.Run();
