using System;
using System.Text;
using Microsoft.Extensions.Logging;

namespace SLException.Core
{
    public sealed class StringLogger : ILogger
    {
        private readonly StringBuilder _sb = new StringBuilder(); 
        
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            _sb.Append(formatter(state, exception));
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return _sb.ToString();
        }
    }
}