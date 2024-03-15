using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationTesting_CorConnect.Resources;
using NLog;

namespace AutomationTesting_CorConnect.applicationContext
{
    internal class LoggerInstance
    {
        private static Logger logger;

        internal static Logger GetLogger()
        {
            if (logger == null)
            {
                logger = LogManager.GetCurrentClassLogger();
            }

            logger.Debug(LoggerMesages.ReturningLoggingContext);
            return logger;
        }
    }
}
