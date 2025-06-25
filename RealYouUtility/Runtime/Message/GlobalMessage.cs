namespace RealYou.Utility.Message
{
    public class GlobalMessage : Message
    {
        private static GlobalMessage _instance;
        
        public static GlobalMessage Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GlobalMessage();
                }

                return _instance;
            }
        }
    }
}