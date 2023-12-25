using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoListAPI.Models;
using ToDoListAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);
string listdbstring= builder.Configuration.GetConnectionString("ListDBString");
string dbcontextstring = builder.Configuration.GetConnectionString("SqlConnection");
// Add services to the container.
builder.Services.AddDbContext<ListDB>(Options => Options.UseSqlServer(listdbstring));
builder.Services.AddDbContext<DBcontext>(Options => Options.UseSqlServer(dbcontextstring));
builder.Services.AddScoped<ItaskRepository, taskRepository>();
builder.Services.AddIdentity<IdentityUser,IdentityRole>(Options => Options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>().AddEntityFrameworkStores<DBcontext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options => {
      options.RequireHttpsMetadata = false;
      options.SaveToken = true;
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT")["Secret"])),
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ValidIssuer = "http://localhost:7141/",
          ValidAudience = "http://localhost:7141/",
      };
  });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
