using Microsoft.Extensions.DependencyInjection;
namespace TaskEvaluator.Modules;

public static class LanguageRegistrationExtension {
    public static IServiceCollection AddLanguage<T>(this IServiceCollection services) where T : LanguageRegistration, new() {
        var registration = new T();
        registration.Register(services, registration.RuntimeFactory);
        registration.Register(services, registration.LanguageService);

        registration.Register(services);

        return services;
    }
}
