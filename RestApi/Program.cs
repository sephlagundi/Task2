using RestApi.Data;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddSwaggerGen();



//Adding CORS in API


//DBCONTEXT
builder.Services.AddDbContext<ApiDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
