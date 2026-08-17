using Microsoft.EntityFrameworkCore;
using TravelerWay.Api.Data;
using TravelerWay.Common.Data.Repositories;
using TravelerWay.Common.Interfaces;
using TravelerWay.Common.Interfaces.Implementations;

namespace TravelerWay.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTravelerWayServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TravelerWayDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddHttpClient<IDuffelService, DuffelService>(client =>
        {
            client.BaseAddress = new Uri(configuration["Duffel:BaseUrl"] ?? "https://api.duffel.com/air");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Duffel-Version", "v2");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration["Duffel:AccessToken"]}");
        });

        // Repositories
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ISearchRepository, SearchRepository>();
        services.AddTransient<IOfferRepository, OfferRepository>();
        services.AddTransient<IAncillaryRepository, AncillaryRepository>();
        services.AddTransient<IPassengerRepository, PassengerRepository>();
        services.AddTransient<IPaymentRepository, PaymentRepository>();
        services.AddTransient<IStripeEventLogRepository, StripeEventLogRepository>();

        // TravelerWay
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<ITravelerWayService, TravelerWayService>();
        services.AddScoped<IStripeService, StripeService>();


        return services;
    }
}
