using System;

namespace RealYou.Utility.Log
{
    public interface ILogOutput
    {
        void Write(LogLevel level, string channel, string message);
    }
    
    /// <summary>
    /// Available logging levels.
    /// </summary>
    public enum LogLevel : int
    {
        /// <summary>
        /// All message will be logged.
        /// </summary>
        Log,

        /// <summary>
        /// Only Warnings and above will be logged.
        /// </summary>
        Warning,

        /// <summary>
        /// Only Errors and above will be logged.
        /// </summary>
        Error,

        /// <summary>
        /// Only Exceptions will be logged.
        /// </summary>
        Exception,

        /// <summary>
        /// No logging will occur.
        /// </summary>
        None
    }
}