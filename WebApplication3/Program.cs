using ApiAtencionesMédicas.Models.Context;
using ApiAtencionesMédicas.Repositorys.JWTRepository.JWTRepository;
using ApiAtencionesMédicas.Services.JWTServices.JWTServices;
using ApiAtencionesMédicas.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//Inyeccion de repositorios
builder.Services.AddScoped<UnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJWTRepository, JWTRepository>();
builder.Services.AddScoped<JWTUtils, JWTUtils>();

//Inyeccion de servicios
builder.Services.AddScoped<IJWTServices, JWTServices>();



builder.Services.AddDbContext<ApiDBContext>(options => options.UseSqlServer(builder.Configuration["ConecctionStrings:AtencionesMedicas"]));
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("mypolicy",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                              .AllowAnyHeader()
                                              .AllowAnyMethod();
                      });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    { Title = "ApiAtencionesMédicas", Version = "v1" });


    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
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
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" },
                        },
                        new string[] {}
                    }
                });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (Directory.Exists(xmlPath))
    {
        File.Create(xmlPath);

    }
    c.IncludeXmlComments(xmlPath);


});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("./v1/swagger.json", "ApiAtencionesMédicas");
    });
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
