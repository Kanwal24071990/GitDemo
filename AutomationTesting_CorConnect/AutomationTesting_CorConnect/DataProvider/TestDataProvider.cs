using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLog;
using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.DataObjects;

namespace AutomationTesting_CorConnect.DataProvider
{
    internal class TestDataProvider
    {
        private static List<MethodInfo> testMethods;
        private static ApplicationContext appContext;
        private static Logger logger;

        public static IEnumerable<object> TestData(string testCaseID)
        {
            //if (appContext == null)
            //{
            //    appContext = ApplicationContext.GetInstance();
            //}

            //if (logger == null)
            //{
            //    logger = LoggerInstance.GetLogger();
            //}

            //logger.Debug(string.Format(LoggerMesages.RetrivingDataForTC, testCaseID));
            //return GetParametersForTC(testCaseID);
            return new List<string>();
        }

        //private static IEnumerable<object> GetParametersForTC(string tcNumber)
        //{
        //    object[] args;
        //    TestCaseData testData = null;
        //    string clientName = ApplicationContext.GetInstance().DefaultClient;

        //    if (testMethods == null)
        //    {
        //        testMethods = new List<MethodInfo>();
        //        Type[] classes = Assembly.LoadFile(Assembly.GetExecutingAssembly().Location).GetTypes();

        //        foreach (Type clas in classes)
        //        {
        //            MethodInfo[] methods = clas.GetMethods();

        //            foreach (MethodInfo methodInfo in methods)
        //            {
        //                if (methodInfo.GetCustomAttributes(typeof(TestAttribute), true).Length == 1)
        //                {
        //                    testMethods.Add(methodInfo);
        //                }
        //            }
        //        }
        //    }


        //    Data TestData = LoadTestData();
        //    var info = testMethods.Where(x => x.Name.Contains(tcNumber)).FirstOrDefault();
        //    string screenName = info.DeclaringType.FullName.Split(".")[2];
        //    var exemptedClients = appContext.skippedScreens.FirstOrDefault(x => x.ScreenName == screenName)?.Clients;
        //    var names = info.GetParameters();
        //    var parametersForTC = TestData.TestData;
        //    List<Dictionary<string, dynamic>> paramList = null;

        //    try
        //    {
        //        paramList = parametersForTC[info.DeclaringType.Name.Replace(" ", "")][tcNumber];
        //    }
        //    catch (KeyNotFoundException e)
        //    {
        //        LoggingHelper.LogException(e);
        //        testData = new TestCaseData();
        //        testData.SetProperty("execute", "Fail");
        //    }

        //    if (paramList == null)
        //    {
        //        args = new object[names.Length];

        //        for (int i = 0; i < names.Length; i++)
        //        {
        //            args[i] = names[i].DefaultValue;
        //        }
        //        testData = new TestCaseData(args);
        //        testData.SetProperty("execute", "Fail Due To invalid araguments");
        //    }
        //    else
        //    {
        //        foreach (var param in paramList)
        //        {
        //            args = new object[names.Length];

        //            try
        //            {
        //                for (int i = 0; i < names.Length; i++)
        //                {
        //                    logger.Debug(string.Format(LoggerMesages.MappingData, names[i].Name, param[names[i].Name]));
        //                    args[i] = Convert.ChangeType(param[names[i].Name], names[i].ParameterType);
        //                }

        //                if (!string.IsNullOrWhiteSpace(Convert.ToString(param["Client"])))
        //                {
        //                    clientName = Convert.ToString(param["Client"]).ToUpper();
        //                }
        //            }
        //            catch (KeyNotFoundException e)
        //            {
        //                LoggingHelper.LogException(e);
        //                testData = new TestCaseData();
        //                testData.SetProperty("execute", "Fail Due To invalid araguments");
        //            }

        //            testData = new TestCaseData(args);
        //            testData.SetDescription("https://determine.atlassian.net/browse/CON-" + tcNumber);
        //            logger.Debug(string.Format(LoggerMesages.AddingTestCaseProperty, "execute", Convert.ToString(param["Execute"])));
        //            testData.SetProperty("execute", Convert.ToString(param["Execute"]).ToUpper());
        //            string userType = Convert.ToString(param["UserType"]).ToUpper();
        //            var users = appContext.ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower()).Users;

        //            if (userType == Constants.UserType.Admin.NameUpperCase)
        //            {
        //                testData.SetProperty("UserName", users.First(u => u.Type == "admin").User);
        //            }
        //            else if (userType == Constants.UserType.Dealer.NameUpperCase)
        //            {
        //                testData.SetProperty("UserName", users.First(u => u.Type == "dealer").User);
        //            }
        //            else if (userType == Constants.UserType.Fleet.NameUpperCase)
        //            {
        //                testData.SetProperty("UserName", users.First(u => u.Type == "fleet").User);
        //            }

        //            testData.SetProperty("Type", userType);
        //            testData.SetProperty("Client", clientName);

        //            if (tcNumber.Contains("22156"))
        //            {

        //            }

        //            if (exemptedClients != null && IsClientUserTypeExempted(exemptedClients, userType))
        //            {
        //                testData.Properties["execute"].RemoveAt(0);
        //                testData.SetProperty("execute", "Screen Not exist");
        //            }
        //            yield return testData;
        //        }
        //    }
        //}


        //private static Data LoadTestData()
        //{
        //    try
        //    {
        //        logger.Debug(LoggerMesages.LoadingTestData);
        //        return appContext.testInputData;
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error(e.ToString());
        //        return null;
        //    }
        //}

        private static bool IsClientUserTypeExempted(List<Client> exemptedClients, string userType)
        {
            if (userType == Constants.UserType.Admin.NameUpperCase)
            {
                return exemptedClients.Any(x => x.ClientName.ToLower() == CommonUtils.GetClientLower() && x.SkipAdminUser);
            }
            Client client = exemptedClients.FirstOrDefault(x => x.ClientName.ToLower() == CommonUtils.GetClientLower());
            if (client != null && !client.SkipAdminUser)
            {
                if (userType == Constants.UserType.Fleet.NameUpperCase)
                {
                    return exemptedClients.Any(x => x.ClientName.ToLower() == CommonUtils.GetClientLower() && x.SkipFleetUser);
                }
                if (userType == Constants.UserType.Dealer.NameUpperCase)
                {
                    return exemptedClients.Any(x => x.ClientName.ToLower() == CommonUtils.GetClientLower() && x.SkipDealerUser);
                }
            }
            return false;
        }
    }
}