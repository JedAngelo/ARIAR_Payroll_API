using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Payroll_Library.Models;
using Payroll_Library.Services.AttendanceServ;
using Payroll_Library.Services.Employee;
using Payroll_Library.UserAuth;
using System.Text;

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
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();


builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultCon")));

//User Auth
builder.Services.AddIdentityCore<ApplicationUser>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireDigit = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddTokenProvider("SuperAdmin", typeof(DataProtectorTokenProvider<ApplicationUser>));

builder.Services.AddAuthentication(opt =>
{
    var _defaultBearer = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultAuthenticateScheme = _defaultBearer;
    opt.DefaultChallengeScheme = _defaultBearer;
    opt.DefaultScheme = _defaultBearer;
})
.AddJwtBearer(opt =>
{
    opt.SaveToken = false;
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]!))
    };
});



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
