using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace LoadChecker
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async System.Threading.Tasks.Task RunAsync([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            string apikey = "lvr2xs8wxy1bafjmzmxdccbtwp6wf1j1uchbd2k2";
            string apiId = "455f8f63-2c37-40d5-8b6e-ac49045ef857";

            RestSharp.RestClient client = new RestSharp.RestClient($"https://api.applicationinsights.io/v1/apps/{apiId}/");

            client.DefaultParameters.Add(new RestSharp.Parameter { Name = "X-API-Key", Value = apikey });

            RestSharp.RestRequest request = new RestSharp.RestRequest(String.Format("metrics/requests/count?timespan=P1D"));
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("X-API-Key", apikey);

            var result = client.ExecuteAsGet(request, "GET");

            if (result.Content.Contains("\"sum\":null"))
            {
                /*
                 * Suspend Resource
                 * 
                 * POST https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.PowerBIDedicated/capacities/{dedicatedCapacityName}/suspend?api-version=2017-10-01
                 * 
                 */ 


            }
            

            

            /*
             * {"value":{"start":"2018-05-28T12:53:01.013Z","end":"2018-05-29T12:53:01.013Z","requests/count":{"sum":null}}}
             * 
             */

                Console.WriteLine(result);
        }
    }
}
