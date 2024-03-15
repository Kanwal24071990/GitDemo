using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ResultsAndHistoryCleanUtility
{
    internal class Program
    {
        static string machineName = System.Environment.MachineName;
        public static string cleanResultsBaseUrl = "http://" + machineName.ToUpper() + ":5050/allure-docker-service/clean-results?project_id=";
        public static string cleanHistoryBaseUrl = "http://" + machineName.ToUpper() + ":5050/allure-docker-service/clean-history?project_id=";
        public static List<string> clients = new List<string>();
        static void Main(string[] args)
        {
            try
            {

                Task t1 = Task.Run(() =>
                {
                    string resultsEndPoint = cleanResultsBaseUrl + Environment.GetEnvironmentVariable("UAT");
                    RestClient resultsClient = new RestClient(resultsEndPoint);
                    RestRequest resultRequest = new RestRequest(resultsEndPoint, Method.Get);
                    RestResponse resultResponse = resultsClient.Execute(resultRequest);
                    if (resultResponse.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("Successfully cleaned results for client: " + Environment.GetEnvironmentVariable("UAT") + ". Below was the response");
                        Console.WriteLine(resultResponse.Content);
                    }
                    else
                    {
                        Console.WriteLine("Failed to clean results for client: " + Environment.GetEnvironmentVariable("UAT") + ". Status Code: " + resultResponse.StatusCode);
                        Console.WriteLine("Error:: " + resultResponse.ErrorMessage);
                    }
                });
                t1.Wait();
                t1.Dispose();

                Task t2 = Task.Run(() =>
                {
                    string historyEndPoint = cleanHistoryBaseUrl + Environment.GetEnvironmentVariable("UAT");
                    RestClient historyClient = new RestClient(historyEndPoint);
                    RestRequest historyRequest = new RestRequest(historyEndPoint, Method.Get);
                    RestResponse historyResponse = historyClient.Execute(historyRequest);
                    if (historyResponse.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("Successfully cleaned history for client: " + Environment.GetEnvironmentVariable("UAT") + ". Below was the response");
                        Console.WriteLine(historyResponse.Content);
                    }
                    else
                    {
                        Console.WriteLine("Failed to clean history for client: " + Environment.GetEnvironmentVariable("UAT") + ". Status Code: " + historyResponse.StatusCode);
                        Console.WriteLine("Error:: " + historyResponse.ErrorMessage);
                    }
                });
                t2.Wait();
                t2.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to clear results and history: " + ex.Message);
            }

        }

    }
}
