using Microsoft.EntityFrameworkCore;
using Payroll_Library.Models;
using Payroll_Library.Services.AttendanceServ;
using Payroll_Library.Services.Employee;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Added by DELTAN
builder.Services.AddDbContext<AriarPayrollDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultCon")));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();

// Added end

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

app.Run();
