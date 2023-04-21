using Netcompany.Net.Cqs;
using Netcompany.Net.ErrorHandling;
using Netcompany.Net.Logging.Serilog;
using Netcompany.Net.UnitOfWork;
using Netcompany.Net.Validation;
using RoutePlanning.Application;
using RoutePlanning.Client.Web.Service;
using RoutePlanning.Domain.Services;
using RoutePlanning.Infrastructure;

namespace RoutePlanning.Client.Web;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRoutePlanningInfrastructure(builder.Configuration);
        builder.Services.AddRoutePlanningApplication();

        builder.Services.AddCqs(options => options.UseValidation().UseUnitOfWork());
        builder.Services.AddUnhandledExceptionMiddleware();
        builder.Services.AddValidationMiddleware();

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        builder.Services.AddSimpleAuthentication();
        builder.Services.AddApiTokenAuthorization(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IBookingService, BookingService>();

        builder.Services.AddSpaStaticFiles(configuration =>
        {
            configuration.RootPath = "./wwwroot/react/build";

        });

        builder.Host.ConfigureLoggingDefaults();

        var app = builder.Build();

        await DatabaseInitialization.SeedDatabase(app);

        if (app.Environment.IsProduction())
        {
            app.UseUnhandledExceptionMiddleware();
        }

        app.MapWhen(x => IsAllowOverride(x), client =>
        {
            client.UseSpaStaticFiles();
            client.UseSpa(spa => { });
        });

        app.UseValidationMiddleware();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");
        app.UseSwagger();
        app.UseSwaggerUI(swagger => {
            swagger.RoutePrefix = "swg";
        });

        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed(origin => true));

        app.Run();
    }
    public static bool IsAllowOverride(HttpContext x)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return !x.Request.Path.Value.StartsWith("/api") && !x.Request.Path.Value.StartsWith("/swg");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
}
