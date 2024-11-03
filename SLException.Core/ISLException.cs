using System;
using Microsoft.Extensions.Logging;

namespace SLException.Core
{
    public interface ISLException
    {
        void LogStructurally(ILogger logger, Exception me);
    }
}