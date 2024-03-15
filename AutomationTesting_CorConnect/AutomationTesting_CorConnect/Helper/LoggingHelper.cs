using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.Resources;
using NLog;
using NUnit.Framework;
using System;

namespace AutomationTesting_CorConnect.Helper
{
    internal class LoggingHelper
    {
        private static Logger Logger = LoggerInstance.GetLogger();

        internal static void LogException(string exceptionDetails)
        {
            Logger.Error(TestContext.CurrentContext.Test.Name + ": " + exceptionDetails);
        }

        internal static void LogException(Exception exceptionDetails)
        {
            Logger.Error(TestContext.CurrentContext.Test.Name + ": " + exceptionDetails);
        }

        internal static void InitializePage(string Page)
        {
            Logger.Debug(TestContext.CurrentContext.Test.Name + ": " + LoggerMesages.InitializingPage + Page);
        }

        internal static void LogMessage( string msg)
        {
            Logger.Debug(TestContext.CurrentContext.Test.Name + ": "+ msg);
        }
    }
}
