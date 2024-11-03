using System;

namespace SLException.Core
{
    internal static class SLExceptionUtils
    {
        public static string FormatExceptionMessage(SLException ex)
        {
            var logger = new StringLogger();
            ex.LogStructurally(logger, ex);
            return logger.ToString();
        }
    }
}