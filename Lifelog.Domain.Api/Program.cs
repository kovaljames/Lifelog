using Lifelog.Domain.Handlers;
using Lifelog.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Lifelog.Domain.Api;
using Lifelog.Domain.Infra.Repositories;
using Lifelog.Domain.Infra.Contexts;

var builder = WebApplication.CreateBuilder(args);

ConfigureAuthentication();
ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureAuthentication()
{
    // Firebase Social Login
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = $"https://securetoken.google.com/{Configuration.Token.FirebaseProject}";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = $"https://securetoken.google.com/{Configuration.Token.FirebaseProject}",
                ValidateAudience = true,
                ValidAudience = Configuration.Token.FirebaseProject,
                ValidateLifetime = true
            };
        });
    /*
    // Generate Local Token instead of Firebase Social Login
    var key = Encoding.ASCII.GetBytes(
        builder.Configuration.GetValue<string>("JwtKey"));
    
    builder.Services
        .AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer("Auth", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });*/
}

void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    // services.AddDbContext<DataContext> (opt => opt.UserInMemoryDatabase("Database"));
    services.AddDbContext<DataContext>(opt => 
        opt.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));

    services.AddTransient<ITaskItemRepository, TaskItemRepository>();
    services.AddTransient<IUserRepository, UserRepository>();
    services.AddTransient<IProjectRepository, ProjectRepository>();
    
    services.AddTransient<TaskItemHandler, TaskItemHandler>();
    services.AddTransient<UserHandler, UserHandler>();
    services.AddTransient<ProjectHandler, ProjectHandler>();
}