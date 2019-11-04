using NLog;

namespace Common
{
    public static class LoggerManager
    {
        private static Logger logger { get; set; }
        public static Logger Logger
        {
            get
            {
                if(logger == null)
                {
                    logger = LogManager.GetCurrentClassLogger();
                }
                return logger;
            }
        }
    }
}
