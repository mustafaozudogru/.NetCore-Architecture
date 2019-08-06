using System;
using System.Collections.Generic;
using System.Text;

namespace CoreArchitectureDesign.Core.Helpers
{
    public static class LoggingExtensions
    {
        public static string WriteLog(this Exception ex, string methodName, string className)
        {
            return $"MethodName => {methodName}() FullName => {className} Message => {ex.Message} InnerException => {ex.InnerException} StackTrace => {ex.StackTrace}";
        }
    }
}
