using Microsoft.EntityFrameworkCore;
using webNET_2024_aspnet_1.Additional_Services.TokenHelpers;
using webNET_2024_aspnet_1.DBContext;
using webNET_2024_aspnet_1.Services.IServices;
using webNET_2024_aspnet_1.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using webNET_2024_aspnet_1.AdditionalServices.TokenHelpers;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{ 
     options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
     {
         Description = "Please enter token",
         Name = "Authorization",
         In = Microsoft.OpenApi.Models.ParameterLocation.Header,
         Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
         Scheme = "Bearer"
     });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

});


builder.Services.AddAuthorization(services =>
{
    services.AddPolicy("TokenBlackListPolicy", policy => policy.Requirements.Add(new TokenBlackListRequirment()));
});


builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<TokenInteraction>();
builder.Services.AddSingleton<IAuthorizationHandler, TokenBlackListPolicy>();
builder.Services.AddScoped<IPatientService, PatientService>();


var secretKey = builder.Configuration["AppSettings:Secret"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
        ValidAudience = builder.Configuration["AppSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
