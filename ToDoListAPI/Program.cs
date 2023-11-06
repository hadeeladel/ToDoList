using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToDoListAPI.Models;

var builder = WebApplication.CreateBuilder(args);
string listdbstring= builder.Configuration.GetConnectionString("ListDBString");
string dbcontextstring = builder.Configuration.GetConnectionString("SqlConnection");
// Add services to the container.
builder.Services.AddDbContext<ListDB>(Options => Options.UseSqlServer(listdbstring));
builder.Services.AddDbContext<DBcontext>(Options => Options.UseSqlServer(dbcontextstring));
builder.Services.AddIdentity<IdentityUser,IdentityRole>(Options => Options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DBcontext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAuthentication();
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
