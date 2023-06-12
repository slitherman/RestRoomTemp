using Microsoft.EntityFrameworkCore;
using RestRoomTemp;
using RestRoomTemp.Models;
using RestRoomTemp.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddControllers();

// Enable Swagger/OpenAPI documentation generation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the database context and repository.

// Create an instance of DbContextOptionsBuilder for RoomTempContext.
var optionbuilder = new DbContextOptionsBuilder<RoomTempContext>();

// Register RoomTempContext as a service and configure it to use SQL Server with the connection string from DbConn class.
builder.Services.AddDbContext<RoomTempContext>(options => options.UseSqlServer(DbConn.Conn));

// Create an instance of RoomTempContext using the configured options.
RoomTempContext context = new RoomTempContext(optionbuilder.Options);

// Register the RestRoomRepoDB as a service, injecting the RoomTempContext instance into its constructor.
builder.Services.AddScoped<IRestRoomRepoDB>(x => new RestRoomRepoDB(x.GetService<RoomTempContext>()));

// Configure CORS (Cross-Origin Resource Sharing) policy.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI in development environment.
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS policy.
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

// Map controllers.
app.MapControllers();

app.Run();
