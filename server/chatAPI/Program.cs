using chatAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
if (builder.Configuration.GetValue<bool>("UseInMemoryDB"))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("AppDB")
    );

    builder.Services.AddDbContext<AuthDbContext>(options =>
        options.UseInMemoryDatabase("AuthDB")
    );
}
else
{
    // Expecting lot more weight on ApplicationDbContext so pooling it seems like a good idea
    // to take initialization perf hit off the request times
    builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("mssql:app"))
    );

    builder.Services.AddDbContext<AuthDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("mssql:auth"))
    );
}


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
else
{
    app.UseHttpsRedirection();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
