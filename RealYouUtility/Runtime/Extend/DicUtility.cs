using System;
using System.Collections.Generic;
using RealYou.Utility.Log;

namespace RealYou.Utility
{
    public static class DicUtility
    {
        private static Logger _logger = new Logger("DicUtility");

        public static int TryGetInt(this Dictionary<string, object> dic, string key, int defaultVal)
        {
            return TryGetInt(dic, key, out var val) ? val : defaultVal;
        }
        
        public static bool TryGetInt(this Dictionary<string, object> dic, string key, out int val)
        {
            val = 0;
            
            if (dic == null)
                return false;

            if (dic.TryGetValue(key, out var temp))
            {
                try
                {
                    if (temp is int vInt)
                    {
                        val = vInt;
                        return true;
                    }

                    if(temp is long vLong)
                    {
                        if (vLong > int.MaxValue)
                        {
                            val = int.MaxValue;
                        }
                        else
                        {
                            val = (int)vLong;
                        }
                        return true;
                    }

                    return int.TryParse(temp.ToString(), out val);
                }
                catch (Exception e)
                {
                    _logger.LogException(e);
                }
            }

            return false;
        }
        
        public static long TryGetLong(this Dictionary<string, object> dic, string key, long defaultVal)
        {
            return TryGetLong(dic, key, out var val) ? val : defaultVal;
        }
        
        public static bool TryGetLong(this Dictionary<string, object> dic, string key, out long val)
        {
            val = 0;
            
            if (dic == null)
                return false;

            if (dic.TryGetValue(key, out var temp))
            {
                try
                {
                    if (temp is long t)
                    {
                        val = t;

                        return true;
                    }

                    if(temp is double doubleVal)
                    {
                        if (doubleVal > long.MaxValue)
                        {
                            val = long.MaxValue;
                        }
                        else
                        {
                            val = (long) doubleVal;
                        }

                        return true;
                    }

                    return long.TryParse(temp.ToString(), out val);

                }
                catch (Exception e)
                {
                    _logger.LogException(e);
                }
            }

            return false;
        }
        
        
        public static float TryGetFloat(this Dictionary<string, object> dic, string key, float defaultVal)
        {
            return TryGetFloat(dic, key, out var val) ? val : defaultVal;
        }
        
        public static bool TryGetFloat(this Dictionary<string, object> dic, string key, out float val)
        {
            val = 0;
            
            if (dic == null)
                return false;

            if (dic.TryGetValue(key, out var temp))
            {
                try
                {
                    if (temp is float t)
                    {
                        val = t;

                        return true;
                    }

                    if (temp is double t2)
                    {
                        val = (float)t2;

                        return true;
                    }

                    return float.TryParse(temp.ToString(), out val);
                }
                catch (Exception e)
                {
                    _logger.LogException(e);
                }
            }

            return false;
        }
        
        public static double TryGetDouble(this Dictionary<string, object> dic, string key, double defaultVal)
        {
            return TryGetDouble(dic, key, out var val) ? val : defaultVal;
        }
        
        public static bool TryGetDouble(this Dictionary<string, object> dic, string key, out double val)
        {
            val = 0;
            
            if (dic == null)
                return false;

            if (dic.TryGetValue(key, out var temp))
            {
                try
                {
                    if (temp is double t)
                    {
                        val = (float)t;

                        return true;
                    }
                    
                    return double.TryParse(temp.ToString(), out val);
                }
                catch (Exception e)
                {
                    _logger.LogException(e);
                }
            }

            return false;
        }
        
        public static string TryGetString(this Dictionary<string, object> dic, string key, string defaultVal)
        {
            return TryGetString(dic, key, out var val) ? val : defaultVal;
        }

        public static bool TryGetString(this Dictionary<string, object> dic, string key, out string val)
        {
            val = string.Empty;

            if (dic == null)
                return false;

            if (dic.TryGetValue(key, out var temp))
            {
                val = temp.ToString();
                return true;
            }

            return false;
        }
    }
}