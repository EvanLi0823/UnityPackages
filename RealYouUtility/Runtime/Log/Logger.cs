using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using RealYou.Utility.Json;
using Object = System.Object;

namespace RealYou.Utility.Log
{
    public class Logger
    {
        public static Logger GlobalLogger { get; }=new Logger("GlobalLogger");

        public static LogLevel Level = LogLevel.Log;

        private static ILogOutput _logOutput = new UnityLogOutput();

        private string _channel;

        public static void SetLogger(ILogOutput logOutput)
        {
            _logOutput = logOutput;
        }
        
        public Logger(string channel)
        {
            _channel = channel;
        }
        
        public void Log(Object val)
        {
            if(Level != LogLevel.Log)
                return;
            
            if(val == null)
                return;
            
            Log(val.ToString());
        }
        
        public void Log(string message)
        {
            if(Level != LogLevel.Log)
                return;
            
            if(string.IsNullOrEmpty(message))
                return;
            
            _logOutput?.Write(LogLevel.Log,_channel,message);
        }

        public void LogFormat(string format, params object[] args)
        {
            if(Level != LogLevel.Log)
                return;
            
            if(string.IsNullOrEmpty(format) || args == null)
                return;
            
            Log(string.Format(format, args));
        }
        
        public void Log(IEnumerable val)
        {
            if(Level != LogLevel.Log)
                return;
            
            if(val == null)
                return;
            
            StringBuilder sb = new StringBuilder();
            foreach (var v in val)
            {
                sb.Append(v.ToString());
            }
            
            Log(sb.ToString());
        }
        
        public void Log<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> val)
        {
            if(Level != LogLevel.Log)
                return;
            
            if(val == null)
                return;

            Log(RealYouJson.Serialize(val));
        }
        
        public void LogException(Exception e)
        {
            if(Level > LogLevel.Exception)
                return;
            
            if(e == null)
                return;

            LogException(e.ToString());
        }
        
        public void LogExceptionFormat(string format, params object[] args)
        {
            if(Level > LogLevel.Exception)
                return;
            
            if(string.IsNullOrEmpty(format))
                return;
            
            _logOutput?.Write(LogLevel.Exception,_channel,string.Format(format,args));
        }
        
        public void LogException(string message)
        {
            if(Level > LogLevel.Exception)
                return;
            
            if(string.IsNullOrEmpty(message))
                return;
            
            _logOutput?.Write(LogLevel.Exception,_channel,message);
        }

        public void Error(string message)
        {
            if(Level > LogLevel.Error)
                return;
            
            if(string.IsNullOrEmpty(message))
                return;
            
            _logOutput?.Write(LogLevel.Error,_channel,message);
        }
        
        public void ErrorFormat(string format, params object[] args)
        {
            if(Level > LogLevel.Error)
                return;
            
            if(string.IsNullOrEmpty(format) || args == null)
                return;
            
            Error(string.Format(format, args));
        }
        
        public void Warning(string message)
        {
            if(Level > LogLevel.Warning)
                return;
            
            if(string.IsNullOrEmpty(message))
                return;
            
            _logOutput?.Write(LogLevel.Warning,_channel,message);
        }
        
        public void WarningFormat(string format, params object[] args)
        {
            if(Level > LogLevel.Warning)
                return;
            
            if(string.IsNullOrEmpty(format) || args == null)
                return;
            
            Warning(string.Format(format, args));
        }
    }
}