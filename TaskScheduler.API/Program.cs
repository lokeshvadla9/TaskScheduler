using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskScheduler.Agent;
using TaskScheduler.Agent.Interfaces;
using TaskScheduler.API.Middleware;
using TaskScheduler.Helper;
using TaskScheduler.Helper.Interfaces;
using TaskScheduler.Repository;
using TaskScheduler.Repository.Interfaces;

namespace TaskScheduler.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container

            // Configure DbContext to use SQL Server
            builder.Services.AddDbContext<TasksDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add controllers and JSON options
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TaskScheduler API", Version = "v1" });

                
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: 'Bearer abc123'"
                });

                
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // Dependency Injection Resolvers
            builder.Services.AddTransient<IUserAgent, UserAgent>();
            builder.Services.AddTransient<ITaskAgent, TaskAgent>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<ITaskRepository,TaskRepository>();
            builder.Services.AddScoped<IJwtUtils, JwtUtils>();

            //  JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    RoleClaimType="Role"
                };
            });

            //  Authorization Policies for Admin and User
            builder.Services.AddAuthorization(options =>
            {
                // Admin-only policy
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));

                // User-only policy
                options.AddPolicy("UserOnly", policy => policy.RequireRole("user"));

                // Policy that allows both Admin and User
                options.AddPolicy("AdminAndUser", policy => policy.RequireRole("admin", "user"));
            });

            //Adding Cors 
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            var app = builder.Build();
            app.UseCors("AllowAll");
            // Using custom JWT Middleware
            app.UseMiddleware<JwtMiddleware>();

            //  Authentication and Authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Configure the HTTP request pipeline for development
            if (app.Environment.IsDevelopment())
            {
                // Enable Swagger for API documentation
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Use HTTPS redirection
            app.UseHttpsRedirection();

            // Map controller routes
            app.MapControllers();

            app.Run();
        }
    }
}
