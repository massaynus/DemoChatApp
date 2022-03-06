using System.Text;
using chatAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var Services = builder.Services;
var Configuration = builder.Configuration;

// Add services to the DI container
if (Configuration.GetValue<bool>("UseInMemoryDB"))
{
    Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("AppDB")
    );

    Services.AddDbContext<AuthDbContext>(options =>
        options.UseInMemoryDatabase("AuthDB")
    );
}
else
{
    // Expecting lot more weight on ApplicationDbContext so pooling it seems like a good idea
    // to take initialization perf hit off the request times
    Services.AddDbContextPool<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("mssql:app"))
    );

    Services.AddDbContext<AuthDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("mssql:auth"))
    );
}

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


app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.Run();
