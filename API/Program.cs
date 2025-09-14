using API;
using Application.Activities.Command;
using Application.Middlewares;
using Application.Repositories;
using Application.Validators;
using Domain.Absractions;
using Domain.IServices;
using Domain.Mediator;
using Domain.Services.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.IdentityEnitities;
using Persistence.Services;
using Persistence.Services.JWTService.Options;
using Persistence.Services.JWTService.Processors;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(
    options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
{
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDBContext>();


builder.Services.AddDbContext<AppDBContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JWTOptions>(
             builder.Configuration.GetSection(JWTOptions.JWTOptionsKey));


builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var jwtOptions = builder.Configuration.GetSection(JWTOptions.JWTOptionsKey)
                  .Get<JWTOptions>() ?? throw new ArgumentException(nameof(JWTOptions));
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["Access_Token"];
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAuthTokenProcessor, AuthTokenProcessor>();
builder.Services.AddScoped<IDeviceContext, DeviceContext>();
builder.Services.AddScoped<ITokenBlacklistService, TokenBlacklistService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateActivityValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EditActivityValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SignupValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SigninValidator>();


builder.Services.AddScoped<IMediator, Mediator>();


builder.Services.Scan(scan => scan 
    .FromAssemblyOf<CreateActivity>()// Scan Application assembly
    .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
    .AsImplementedInterfaces()
    .WithScopedLifetime());


//builder.Services.AddScoped<IPipelineBehavior<CreateActivity.Command, OperationResult<Guid>>, ValidationBehavior<CreateActivity.Command, OperationResult<Guid>>>();

//builder.Services.AddTransient<ExceptionMiddleware>();


builder.Services.Scan(scan => scan
    .FromAssemblyOf<ActivityRepository>() 
    .AddClasses(classes => classes.AssignableTo(typeof(IRepositoty<>)))
    .AsImplementedInterfaces()
    .WithScopedLifetime());

builder.Services.AddHostedService<DataBaseSeeder>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader().AllowCredentials());
});

builder.Services.AddDistributedMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at root (http://localhost:5000)
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowReactApp");

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<DeviceIdMiddleWare>();

app.MapControllers();

app.MapGroup("api");

app.Run();
