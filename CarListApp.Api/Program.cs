using CarListApp.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o => {
    o.AddPolicy("AllowAll", a => a.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

// this should be outside of the GetCurrentDirectory() folder when we publish, just in case there are permission issues
//var dbPath = Path.Join(Directory.GetCurrentDirectory(), "carList.db");
var dbPath = "C:\\ForAppTesting\\carlist.db";
var connection = new SqliteConnection($"DataSource={dbPath}");
builder.Services.AddDbContext<CarListDbContext>(o => o.UseSqlite(connection));

// IdentityUser is the (default) base class, we can extend it
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CarListDbContext>();

// jwt settings
// this configures authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => 
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,      // validate that the Issuer matches whats defined in appsettings
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidateAudience = true,    // validate that the Audience matches whats defined in appsettings
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidateLifetime = true,    // validate that the token is not expired (lifetime of the token)
        ClockSkew = TimeSpan.Zero,  // set the clock to 0, no grace period for token expiration
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

// global authorization fallback policy
builder.Services.AddAuthorization(options => 
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// OpenAPI stuff
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// security middleware (authentication before authorize)
app.UseAuthentication();
app.UseAuthorization();

// api
app.MapGet("/api/cars", async (CarListDbContext ctx) => await ctx.Cars.ToListAsync());

app.MapGet("/api/cars/{id}", async (int id, CarListDbContext ctx) =>
    await ctx.Cars.FindAsync(id) is Car car ? Results.Ok(car) : Results.NotFound()
);

app.MapPut("/api/cars/{id}", async (int id, Car car, CarListDbContext ctx) => {
    var record = await ctx.Cars.FindAsync(id);

    if (record is null)
    {
        return Results.NotFound();
    }

    record.Make = car.Make;
    record.Model = car.Model;
    record.Vin = car.Vin;

    await ctx.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/api/cars/{id}", async (int id, CarListDbContext ctx) =>
{
    var record = await ctx.Cars.FindAsync(id);

    if (record is null)
    {
        return Results.NotFound();
    }

    ctx.Remove(record);
    await ctx.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPost("/api/cars", async (Car car, CarListDbContext ctx) => {
    await ctx.AddAsync(car);
    await ctx.SaveChangesAsync();

    return Results.Created($"/api/cars/{car.Id}", car);
});

// disable authorization on this endpoint
app.MapPost("/login", async (LoginDto loginDto, UserManager<IdentityUser> userManager) => {
    var user = await userManager.FindByNameAsync(loginDto.Username);
    if (user is null)
    {
        return Results.Unauthorized();
    }

    var isValidPassword = await userManager.CheckPasswordAsync(user, loginDto.Password);
    if(!isValidPassword)
    {
        return Results.Unauthorized();
    }

    // generate token...
    var response = new AuthResponseDto()
    {
        Username = user.UserName,
        UserId = user.Id,
        AccessToken = "AccessToken"
    };

    return Results.Ok(response);
}).AllowAnonymous();

// execute
app.Run();

public class LoginDto
{
    public string Username { get; set; }

    public string Password { get; set; }
}

public class AuthResponseDto
{
    public string Username { get; set; }
    
    public string UserId { get; set; }

    public string AccessToken { get; set; }
}