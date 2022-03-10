using System.Text;
using chatAPI.Data;
using chatAPI.Repositories;
using chatAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using chatAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var Services = builder.Services;
var Configuration = builder.Configuration;

// Add services to the DI container
if (Configuration.GetValue<bool>("UseInMemoryDB"))
{
    Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("AppDB")
    );
}
else
{
    // Expecting lot more weight on ApplicationDbContext so pooling it seems like a good idea
    // to take initialization perf hit off the request times
    Services.AddDbContextPool<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("AppDB"))
    );
}

// Configuring JWT validation options
Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Configuration["Jwt:Issuer"],
            ValidAudience = Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
        };
    });

// Configure AutoMapper
Services.AddAutoMapper(config => {
    config.AddProfile<Mappers>();
});

// Injecting the Repositories
Services.AddTransient<IUserRepository, UserRepository>();
Services.AddTransient<IAuthRepository, AuthRepository>();


// Adding other services
Services.AddTransient<IAuthService, AuthService>();
Services.AddTransient<IUserService, UserService>();
Services.AddSingleton<CryptoService>();
Services.AddSingleton<JwtService>();

// MVC Stuff
Services.AddCors();
Services.AddControllers();
Services.AddEndpointsApiExplorer();
Services.AddSwaggerGen();

// Building app HTTP pipeline
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseCors();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.Run();
