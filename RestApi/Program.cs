using RestApi.Data;
using Microsoft.EntityFrameworkCore;
using RestApi.CustomMiddleware;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        //policy.AllowAnyOrigins()
        policy.WithOrigins("https://localhost:5281", "mydomain.com")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();




//Adding ApiKey textbox in API to authenticate the user
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "The API Key to access the API",
        Type = SecuritySchemeType.ApiKey,
        Name = "ApiKey",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

var scheme = new OpenApiSecurityScheme
{
    Reference = new OpenApiReference
    {
        Type = ReferenceType.SecurityScheme,
        Id = "ApiKey"
    },
    In = ParameterLocation.Header
};

var requirement = new OpenApiSecurityRequirement
{
    { scheme, new List<string>() }
};

c.AddSecurityRequirement(requirement);
});










//DBCONTEXT
builder.Services.AddDbContext<ApiDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Adding CORS in API
app.UseCors(MyAllowSpecificOrigins);

app.UseMiddleware<ApiKeyAuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
