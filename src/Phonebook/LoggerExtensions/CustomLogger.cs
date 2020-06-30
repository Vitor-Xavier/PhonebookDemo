using Microsoft.Extensions.Logging;
using Phonebook.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Phonebook.LoggerExtensions
{
    public class CustomLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            var message = formatter(state, exception);
            if (string.IsNullOrWhiteSpace(message)) return;

            Task.Run(() => FileHelper.WriteLogFile(Path.Combine(Path.GetTempPath(), "Phonebook_log.txt"), $"{DateTime.Now} {logLevel} {message} {exception?.StackTrace}")).ConfigureAwait(false);
        }
    }
}
