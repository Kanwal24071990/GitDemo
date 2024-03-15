using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.Resources;
using Newtonsoft.Json;
using NLog;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System.Globalization;

namespace AutomationTesting_CorConnect.applicationContext
{
    public class ApplicationContext
    {
        internal Stack<IWebDriver> DriverStack;
        //internal Data testInputData;
        internal List<ClientConfiguration> ClientConfigurations;
        internal Dictionary<string, List<string>> PageHeaders;
        internal List<SynonymException> SynonymExceptions;
        internal List<TestDriverProvider> DriverList;
        internal static Logger Logger { get; set; }

        // singleton partern variable
        private static ApplicationContext instance;

        private string EnvironmentFile { get; set; } = @"EnvironmentProperties//environment.properties";
        //private string InputFile { get; set; } = @"TestData/TestInputData.xlsx";
        private string PageHeadersFile { get; set; } = @"TestData/PageHeaders.xlsx";
        private string ClientConfiguration { get; set; } = @"EnvironmentProperties//ClientConfiguration.json";
        //private string SkippedScreens { get; set; } = @"EnvironmentProperties//SkippedScreens.json";
        private string DataBaseConnectionStrings { get; set; } = @"EnvironmentProperties//ConnectionStrings.json";
        private string SynonymExceptionFile { get; set; } = @"EnvironmentProperties//SynonymExceptions.json";
        // enviromentProperties
        public static string Browser { get; set; }
        public int TimeOut { get; set; }
        internal UserData UserData { get; set; }
        internal UserData ImpersonatedUserData { get; set; } = null;
        public string Language { get; set; }
        internal SqlConnection DbConnection { get; set; }
        internal List<Screen> skippedScreens { get; set; }
        internal string client { get; set; }
        internal string MethodName { get; set; }
        internal string TestClassName { get; set; }
        public string downloadsDirectory { get; set; }
        internal string PageObjectPath { get; set; }
        internal string DefaultClient { get; set; } = "FunctionalAutomation";
        internal string ClientDateFormat { get; set; }
        internal string ClientGridDateFormat { get; set; }
        internal bool AdminUser { get; set; } = true;
        internal bool FleetUser { get; set; } = true;
        internal bool DealerUser { get; set; } = true;
        internal string ServerName { get; set; }
        internal int RedisPort { get; set; }
        internal static string RemoteUser { get; set; }
        internal static string RemoteDomain { get; set; }
        internal static string RemotePassword { get; set; }

        internal CultureInfo CurrentCultureInfo { get; set; }

        private ApplicationContext()
        {
            //DriverStack = new Stack<IWebDriver>();
            DriverList = new List<TestDriverProvider>();
            LoadEnviromentProperties();
            //ReadInputData();
            GetClientConfiguration();
            GetDBConfig();
            //skippedScreens = LoadExemptedPagesList();
            LoadPageHeaders();
            LoadSynonymExceptions();

        }

        public static ApplicationContext GetInstance()
        {
            if (Logger == null)
            {
                Logger = LoggerInstance.GetLogger();
            }

            if (instance == null)
            {

                Logger.Debug(LoggerMesages.CreatingNewApplicationContext);
                instance = new ApplicationContext();
            }

            Logger.Debug(LoggerMesages.ReturningApplicationContext);
            return instance;
        }

        private void LoadEnviromentProperties()
        {
            Logger.Debug(LoggerMesages.LoadingEnviormentData);

            // Load environment file
            Hashtable enviromentData = GetEnvironmentData();

            if (enviromentData != null)
            {
                if (enviromentData.ContainsKey("browser"))
                {
                    Browser = (string)enviromentData["browser"];
                }

                if (enviromentData.ContainsKey("language"))
                {
                    Language = (string)enviromentData["language"];
                }

                if (enviromentData.ContainsKey("downloadsDirectory"))
                {
                    downloadsDirectory = (string)enviromentData["downloadsDirectory"];
                }
                else
                {
                    downloadsDirectory = GetDefaultDownloadsPath();
                }

                if (Environment.GetEnvironmentVariable("UAT") != null)
                {
                    client = Environment.GetEnvironmentVariable("UAT");
                }
                else if (enviromentData.ContainsKey("Environment") && !string.IsNullOrEmpty((string)enviromentData["Environment"]))
                {
                    client = (string)enviromentData["Environment"];
                }
                else
                {
                    client = DefaultClient;
                }

                if (enviromentData.ContainsKey("PageObjectPath"))
                {
                    PageObjectPath = (string)enviromentData["PageObjectPath"];
                }

                if (Environment.GetEnvironmentVariable("adminUser") != null)
                {
                    AdminUser = bool.Parse(Environment.GetEnvironmentVariable("adminUser").ToString());
                }
                if (Environment.GetEnvironmentVariable("fleetUser") != null)
                {
                    FleetUser = bool.Parse(Environment.GetEnvironmentVariable("fleetUser").ToString());
                }
                if (Environment.GetEnvironmentVariable("dealerUser") != null)
                {
                    DealerUser = bool.Parse(Environment.GetEnvironmentVariable("dealerUser").ToString());
                }

                if (enviromentData.ContainsKey("RemoteUser"))
                {
                    RemoteUser = (string)enviromentData["RemoteUser"];
                }

                if (enviromentData.ContainsKey("RemoteDomain"))
                {
                    RemoteDomain = (string)enviromentData["RemoteDomain"];
                }

                if (enviromentData.ContainsKey("RemotePassword"))
                {
                    RemotePassword = (string)enviromentData["RemotePassword"];
                }

                if (Environment.GetEnvironmentVariable("ServerName") != null)
                {
                    ServerName = Environment.GetEnvironmentVariable("ServerName");
                }
                else if (enviromentData.ContainsKey("ServerName") && !string.IsNullOrEmpty((string)enviromentData["ServerName"]))
                {
                    ServerName = (string)enviromentData["ServerName"];
                }

                if (Environment.GetEnvironmentVariable("RedisPort") != null)
                {
                    RedisPort = int.Parse(Environment.GetEnvironmentVariable("RedisPort"));
                }
                else if (enviromentData.ContainsKey("RedisPort") && !string.IsNullOrEmpty((string)enviromentData["RedisPort"]))
                {
                    RedisPort = int.Parse(enviromentData["RedisPort"].ToString());
                }
                if (Environment.GetEnvironmentVariable("Culture") != null)
                {
                    string name = Environment.GetEnvironmentVariable("Culture");
                    Logger.Debug($"Setting culture [{name}] from agent's environment variable");
                    CurrentCultureInfo = LoadCurrentCutlureInfo(name);
                }
                else if (enviromentData.ContainsKey("Environment") && !string.IsNullOrEmpty((string)enviromentData["Environment"]))
                {
                    string name = (string)enviromentData["Culture"];
                    Logger.Debug($"Setting culture [{name}] from environment properties");
                    CurrentCultureInfo = LoadCurrentCutlureInfo(name);
                }
                else
                {
                    Logger.Debug($"Setting default  culture");
                    CurrentCultureInfo = LoadCurrentCutlureInfo();
                }

            }
            else
            {
                Logger.Debug(LoggerMesages.EmptyEnviormentData);
            }
        }

        private void GetClientConfiguration()
        {
            using (StreamReader r = new StreamReader(ClientConfiguration))
            {
                string jsonString = r.ReadToEnd();
                ClientConfigurations = JsonConvert.DeserializeObject<List<ClientConfiguration>>(jsonString);
            }
        }

        private void GetDBConfig()
        {
            DataBase.GetDBConfig(DataBaseConnectionStrings);
        }

        //private void ReadInputData()
        //{
        //    Logger.Debug(LoggerMesages.ReadingTestInputData);

        //    try
        //    {
        //        ExcelParser parser = new();
        //        testInputData = parser.Read(InputFile, Logger);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.Message);
        //    }
        //}

        private void LoadPageHeaders()
        {
            Logger.Debug("Loading  PageHeaders from file");

            try
            {
                ExcelParser parser = new();
                PageHeaders = parser.GetPageHeadersDictionaryByClient(PageHeadersFile, client);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
        }


        private Hashtable GetEnvironmentData()
        {
            Hashtable enviromentData = LoadProperties();
            return enviromentData;
        }

        public Hashtable LoadProperties()
        {
            StreamReader properties = new(EnvironmentFile);
            Hashtable fileData = new();
            string line;

            try
            {
                while ((line = properties.ReadLine()) != null)
                {
                    string[] lineData = line.Split(new char[] { '=' }, 2);

                    if (!string.IsNullOrEmpty(lineData[0].Trim()) && !lineData[0].Trim().StartsWith("#"))
                    {
                        fileData.Add(lineData[0].Trim(), lineData[1].Trim());
                    }
                }

                properties.Close();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return fileData;
        }

        private string GetDefaultDownloadsPath()
        {
            string userName = Environment.UserName;
            string path = @"C:\Users\" + userName + @"\Downloads";

            return path;
        }

        public string GetTestContextProperty(string propertyName)
        {
            var property = TestContext.CurrentContext.Test.Properties[propertyName]?.First();
            if (property != null)
            {
                return property.ToString().ToUpper();
            }
            return null;
        }

        //private List<Screen> LoadExemptedPagesList()
        //{
        //    try
        //    {
        //        using (StreamReader r = new StreamReader(SkippedScreens))
        //        {
        //            string jsonString = r.ReadToEnd();
        //            return JsonConvert.DeserializeObject<List<Screen>>(jsonString);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        private void LoadSynonymExceptions()
        {
            using (StreamReader r = new StreamReader(SynonymExceptionFile))
            {
                string jsonString = r.ReadToEnd();
                SynonymExceptions = JsonConvert.DeserializeObject<List<SynonymException>>(jsonString);
            }
        }

        private CultureInfo LoadCurrentCutlureInfo(string cultureName = "en-US")
        {
            return new CultureInfo(cultureName);
        }
    }
}