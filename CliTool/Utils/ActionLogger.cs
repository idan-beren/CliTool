using Microsoft.Extensions.Logging;

namespace CliTool.Utils
{
    public class ActionLogger(string category, LogLevel minLevel = LogLevel.Information) : ILogger
    {
        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            var timestamp = DateTime.Now.ToString("HH:mm:ss");
            var message = formatter(state, exception);

            var logLine = $"{timestamp} [{logLevel}] ({category}) {message}";
            if (exception != null)
            {
                logLine += Environment.NewLine + exception;
            }

            Console.WriteLine(logLine);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= minLevel;
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return NullScope.Instance;
        }

        private class NullScope : IDisposable
        {
            public static readonly NullScope Instance = new();
            public void Dispose() { }
        }

        public override string ToString()
        {
            return $"Category: {category} MinLevel: {minLevel}";
        }
    }
}