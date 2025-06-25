using System;
using RealYou.Utility.Log;

namespace RealYou.Utility.Json
{
    public class RealYouJson
    {
        private static Logger _logger = new Logger("RealYouJson");
        
        public static object Deserialize(byte[] data)
        {
            if (data == null || data.Length < 1)
                return null;
            
            return Deserialize(System.Text.Encoding.UTF8.GetString(data));
        }
        
        
        public static object Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;

            try
            {
                return MiniJson.Deserialize(json);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return null;
            }
        }

        public static string Serialize(object obj,
            bool humanReadable = false,
            int indentSpaces = 2)
        {
            try
            {
                return MiniJson.Serialize(obj,humanReadable,indentSpaces);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return null;
            }
            
        }
    }
}