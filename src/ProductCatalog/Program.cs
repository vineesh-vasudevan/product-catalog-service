
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ProductCatalog.Products.Infrastructure.Mappings;
using ProductCatalog.Products.Infrastructure.Repositories;
using ProductCatalog.Common.Behaviors;
using ProductCatalog.Common.Exceptions.Handler;
using ProductCatalog.Common.Behaviors;
using ProductCatalog.Products.Infrastructure.Mappings;
using ProductCatalog.Products.Infrastructure.Repositories;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .CreateBootstrapLogger();

try
{
    Log.Information("Engine starting up...");

    var assembly = typeof(Program).Assembly;
    var builder = WebApplication.CreateBuilder(args);

    // Configure Serilog with full settings
    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console();
    });

    // Register services
    builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddCarter();
    builder.Services.AddValidatorsFromAssembly(assembly);

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CorrelationIdBehavior<,>));

    builder.Services.AddMediatR(config =>
    {
        config.RegisterServicesFromAssembly(assembly);
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));

    });
    builder.Services.AddExceptionHandler<CustomExceptionHandler>();
    var connection = builder.Configuration.GetConnectionString("Database");


    builder.Services.AddMarten(opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    }).UseLightweightSessions();

    // Add Health Checks
    builder.Services.AddHealthChecks();
    builder.Services.AddHealthChecks()
        .AddNpgSql(connection!);

    var app = builder.Build();

    // Add structured request logging and include correlation ID
    app.UseSerilogRequestLogging(options =>
    {
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            if (httpContext.Request.Headers.TryGetValue("X-Correlation-Id", out var correlationId))
            {
                diagnosticContext.Set("CorrelationId", correlationId.ToString());
            }
        };
    });

    app.MapCarter();

    // Add exception handling middleware --> uses CustomExceptionHandler
    app.UseExceptionHandler(options => { });

    app.UseHealthChecks("/health",
        new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

    app.Run();

}
catch (Exception exception)
{

    Log.Fatal(exception, "Application startup failed!");
    throw;
}

finally
{
    Log.CloseAndFlush();
}