using Microsoft.Extensions.Logging;
using System;

namespace Phonebook.LoggerExtensions
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        private bool _disposed = false;

        public ILogger CreateLogger(string categoryName) => new CustomLogger();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
        }
    }
}
