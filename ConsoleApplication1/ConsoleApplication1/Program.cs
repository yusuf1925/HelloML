// This code requires the Nuget package Microsoft.AspNet.WebApi.Client to be installed.
// Instructions for doing this in Visual Studio:
// Tools -> Nuget Package Manager -> Package Manager Console
// Install-Package Microsoft.AspNet.WebApi.Client

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CallRequestResponseService
{

    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            InvokeRequestResponseService().Wait();
        }

        static async Task InvokeRequestResponseService()
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"no-of-cylinder", "normalized-loses", "engine-type", "brand", "engine-size", "engine-location", "width", "hp", "peak-rpm", "curb-weight"},
                               // Values = new string[,] {  { "eight", "0", "dohc", "alfa-romero", "0", "front", "0", "0", "0", "0" },  { "eight", "0", "dohc", "alfa-romero", "0", "rear", "0", "0", "0", "0" },  }
                             // Values = new string[,] {  { "six", "200", "ohc", "subaru", "200", "rear", "64", "145", "6500", "1000" } }
                             // Values = new string[,] {  { "eight", "90", "dohc", "mercury", "240", "front", "64", "145", "6500", "1000" } }
                             Values = new string[,] {  { "five", "120", "dohcv", "audi", "300", "rear", "70", "280", "6500", "4050" } }

                               //4073
                               //11531
                               //28283
                                /* ----num-of-cylinders:         eight, five, four, six, three, twelve, two.----
                               ---normalized-losses:        continuous from 65 to 256.------
                                ----engine-type:              dohc, dohcv, l, ohc, ohcf, ohcv, rotor.----
                                ----brand:        alfa-romero, audi, bmw, chevrolet, dodge, honda,
                                                 isuzu, jaguar, mazda, mercedes-benz, mercury,
                                                mitsubishi, nissan, peugot, plymouth, porsche,
                                                renault, saab, subaru, toyota, volkswagen, volvo----
                                   ------ engine-size:              continuous from 61 to 326.---------
                                     -------engine-location:          front, rear.------
                             
                                     --------width:                    continuous from 60.3 to 72.3.------------
                                 ------hp               continuous from 48 to 288.-------
                                    ----------peak-rpm:                 continuous from 4150 to 6600.-----------
                                    -------curb-weight:              continuous from 1488 to 4066.---------------- */

                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/a8aaaaaa/services/aaaaaaaaa/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
                System.Threading.Thread.Sleep(30008800);//görmek için beklettik
            }
        }
    }
}
