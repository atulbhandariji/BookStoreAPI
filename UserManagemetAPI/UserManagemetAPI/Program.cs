using UserManagemetAPI.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//add database
builder.Services.AddDbContext<EmployeeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConEmp") ??
    throw new InvalidOperationException("Connection string 'ConEmp' not found.")));
//add cors
builder.Services.AddCors(cors => cors.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseCors("MyPolicy");
app.Run();
