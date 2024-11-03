using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace SLException.Core
{
    public static class SLExceptionLoggerExtensions
    {
        public static void Log(this ILogger logger, SLException ex)
        {
            // Fastest case 1st
            if (ex.InnerException == null)
            {
                ex.LogStructurally(logger, ex);
                return;
            }
            
            // Case with just one inner exception
            var innerEx = ex.InnerException;
            if (innerEx.InnerException == null)
            {
                logger.LogExceptionWithoutInner(innerEx);
                ex.LogStructurally(logger, ex);
                return;
            }
            
            // More inner exceptions... we use a stack :-/
            var stack = new Stack<Exception>(3);
            stack.Push(ex);
            do
            {
                stack.Push(innerEx);
                innerEx = innerEx.InnerException;
            } while (innerEx != null);

            do
            {
                var popEx = stack.Pop();
                logger.LogExceptionWithoutInner(popEx);
            }
            while (stack.Count > 0);
        }
        
        internal static void LogNonSLException(this ILogger logger, Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }

        internal static void LogExceptionWithoutInner(this ILogger logger, Exception ex)
        {
            if (ex is SLException slEx)
            {
                slEx.LogStructurally(logger, slEx);    
            }
            else
            {
                logger.LogNonSLException(ex);
            }
        }
    }
}