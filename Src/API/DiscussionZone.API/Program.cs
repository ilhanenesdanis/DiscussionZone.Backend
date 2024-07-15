using DiscussionZone.API.Filters;
using DiscussionZone.API.IOC;
using DiscussionZone.API.Middleware;
using DiscussionZone.Application.Extensions;
using DiscussionZone.Application.Validators.Category;
using DiscussionZone.Infrasracture.IOC;
using DiscussionZone.Persistence.IOC;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ValidationFilter>();
})
.AddFluentValidation(conf => conf.RegisterValidatorsFromAssemblyContaining<CategoryValidator>())
.ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddApplicationDependency();
builder.Services.AddInfrasractureService();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddSerilogConfiguration(builder.Configuration, builder.Host);
builder.Services.AddIdentityConfiguration();
builder.Services.AddHealthCheckConfiguration(builder.Configuration);

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
