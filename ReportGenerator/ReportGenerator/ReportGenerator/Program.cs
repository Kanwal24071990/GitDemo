using Newtonsoft.Json;
using RestSharp;

var request = new RestRequest();
request.Timeout = 1000000000;

HttpClient client = new HttpClient();
client.Timeout = TimeSpan.FromSeconds(50000);
string machineName = System.Environment.MachineName;
string requestUrl = "http://" + machineName.ToLower() + ":5050/allure-docker-service/generate-report";

if (Environment.GetEnvironmentVariable("UAT") != null)
{
    requestUrl = "http://" + machineName.ToLower() + ":5050/allure-docker-service/generate-report?project_id=" + Environment.GetEnvironmentVariable("UAT").ToLower();
}
Console.WriteLine(System.Environment.MachineName);
Console.WriteLine(requestUrl);

HttpResponseMessage? result = await client.GetAsync(requestUrl);


var response = await result.Content.ReadAsStringAsync();

Console.WriteLine(response);
var json = JsonConvert.DeserializeObject<Root>(response);
Console.WriteLine(json.data.report_url);



public class Data
{
    public List<string> allure_results_files { get; set; }
    public string report_url { get; set; }
}

public class MetaData
{
    public string message { get; set; }
}

public class Root
{
    public Data data { get; set; }
    public MetaData meta_data { get; set; }
}
