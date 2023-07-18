//1. Usings to work with EntityFramework
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversityAPI;
using UniversityAPI.DataAcces;
using UniversityAPI.Services;

var builder = WebApplication.CreateBuilder(args);

//2. Conection with database: SQL Server Express
const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// 3.  Add context 
builder.Services.AddDbContext<UniversityContext>(options => options.UseSqlServer(connectionString));


//4. Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//5. Add Custom Services
builder.Services.AddScoped<IStudentServices, StudentServices>();

//7. Add services JWT 
builder.Services.AddJwtTokenServices(builder.Configuration);

//8. Add authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
});

//9. Add swagger config to get care of autentication
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorazatin header using bearer scheme"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id ="Bearer"
            }
        },
        new string[]{}
        }
    });
});

//6.CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//6.1. Tell app to use CORS
app.UseCors("CorsPolicy");
app.Run();