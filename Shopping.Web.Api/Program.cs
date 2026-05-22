using Shopping.Web.Api.Middleware;
using Shopping.Web.Repository.Extensions;
using Shopping.Web.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Register Repository Layer services
builder.Services.AddRepositoryServices(builder.Configuration);

// Register Service Layer services
builder.Services.AddServiceLayerServices();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Use global exception handling middleware
app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
