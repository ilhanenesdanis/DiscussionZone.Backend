using DiscussionZone.API.Middleware;
using DiscussionZone.Persistence.IOC;
using HealthChecks.UI.Client;
using Serilog;
using Serilog.Core;
using DiscussionZone.Application.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddApplicationDependency();

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
app.UseMiddleware<ApiKeyAuthenticationMiddleware>();

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
