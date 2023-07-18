//1. Usings to work with EntityFramework
using Microsoft.EntityFrameworkCore;
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