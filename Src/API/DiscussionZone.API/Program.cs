using DiscussionZone.API.Filters;
using DiscussionZone.API.Middleware;
using DiscussionZone.Application.DTOs;
using DiscussionZone.Application.Extensions;
using DiscussionZone.Application.Validators.Category;
using DiscussionZone.Application.Validators.User;
using DiscussionZone.Domain;
using DiscussionZone.Persistence.Context;
using DiscussionZone.Persistence.IOC;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;
using System.Text;
using DiscussionZone.Infrasracture.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ValidationFilter>();
})
.AddFluentValidation(conf => conf.RegisterValidatorsFromAssemblyContaining<CategoryValidator>())
.ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();


var jwtSettings = builder.Configuration.GetSection("JWT").Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings?.Key ?? string.Empty);



builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;//
    x.SaveToken = true;//
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddApplicationDependency();
builder.Services.AddInfrasractureService();

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "ApplicationLogs",
                        needAutoCreateTable: true)
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .WriteTo.Seq(builder.Configuration.GetSection("SeqUrl")?.Value ?? string.Empty)
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddIdentity<AppUser, IdentityRole>(conf =>
{
    conf.Password.RequiredLength = 6;
    conf.Password.RequireNonAlphanumeric = true;
    conf.Password.RequireLowercase = true;
    conf.Password.RequireUppercase = true;
    conf.Password.RequireDigit = true;
    conf.User.RequireUniqueEmail = true;


    conf.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    conf.Lockout.MaxFailedAccessAttempts = 5;
    conf.Lockout.AllowedForNewUsers = true;

    conf.SignIn.RequireConfirmedEmail = true;
    conf.SignIn.RequireConfirmedPhoneNumber = false;

})
.AddEntityFrameworkStores<AppDbContext>()
.AddPasswordValidator<CustomPasswordValidator>()
.AddDefaultTokenProviders();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("PostgreSQL") ?? string.Empty);

builder.Services.AddHealthChecksUI()
    .AddPostgreSqlStorage(builder.Configuration.GetConnectionString("PostgreSQL") ?? string.Empty);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/json");
    logging.ResponseBodyLogLimit = 4096;
    logging.RequestBodyLogLimit = 4096;
});

var app = builder.Build();

app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHealthChecksUI(opt =>
{
    opt.UIPath = "/health-ui";
});

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
//app.UseMiddleware<ApiKeyAuthenticationMiddleware>();

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
