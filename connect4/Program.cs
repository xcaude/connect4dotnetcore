using Connect4Game.Infrastructure.Context;
using Connect4Game.Common.Dto;
using Connect4Game.Domain.Model;

using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

using Microsoft.VisualBasic;
using System.Reflection.Metadata;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("Connect4Database");

builder.Services.AddDbContext<Connect4DbContext>(options =>
    options.UseSqlite(connString));
    
builder.Services.AddIdentity<Player, IdentityRole>()
    .AddEntityFrameworkStores<Connect4DbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllers();  // Add controllers

builder.Services.AddAuthentication(option  => {
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options => {
       options.TokenValidationParameters = new TokenValidationParameters
       {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
       };
       options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token validated successfully.");
            return Task.CompletedTask;
        }
    };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var jwtsecurityscheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Enter you JWT token",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, jwtsecurityscheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtsecurityscheme, Array.Empty<string>() }
    });
}
);

builder.Logging.AddConsole();
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var playerController = scope.ServiceProvider.GetRequiredService<UserManager<Player>>();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();

