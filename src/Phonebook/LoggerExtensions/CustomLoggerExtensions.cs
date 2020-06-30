using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Phonebook.LoggerExtensions
{
    public static class CustomLoggerExtensions
    {
        public static ILoggingBuilder AddCustomLogger(this ILoggingBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, CustomLoggerProvider>());

            return builder;
        }
    }
}
