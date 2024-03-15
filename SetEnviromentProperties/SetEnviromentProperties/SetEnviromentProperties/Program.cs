

if (Environment.GetEnvironmentVariable("UAT")!=null && Environment.GetEnvironmentVariable("UAT").ToLower() != default)
{
    string machineName = System.Environment.MachineName;
    string pathPapa01 = @"C:\agents\_work\1\s\AutomationTesting_CorConnect\AutomationTesting_CorConnect\allureConfig.json";
    string pathPapa02 = @"C:\agentnew\_work\3\s\AutomationTesting_CorConnect\AutomationTesting_CorConnect\allureConfig.json";
    Console.WriteLine($"Setting environment variable Environment.GetEnvironmentVariable(\"UAT\") [{Environment.GetEnvironmentVariable("UAT")}]");
    if (machineName == "C0PAPA01-USPA01")
    {
        string json = File.ReadAllText(pathPapa01);
        dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        jsonObj["allure"]["directory"] = jsonObj["allure"]["directory"].ToString().Replace("default", Environment.GetEnvironmentVariable("UAT"));
        string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(pathPapa01, output);
    }
    else if (machineName == "C0PAPA04-USPA01") 
    {
        string json = File.ReadAllText(pathPapa02);
        dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        jsonObj["allure"]["directory"] = jsonObj["allure"]["directory"].ToString().Replace("default", Environment.GetEnvironmentVariable("UAT"));
        string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(pathPapa02, output);
    }
    
    
}