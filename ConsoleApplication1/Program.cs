using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication1
{
    using System.Net.Http;

    using AlienAttack;

    using Newtonsoft.Json;

    class Program
    {
        static void Main(string[] args)
        {
            const string baseUri = "http://goserver.cloudapp.net:3000/{0}";
            const string email = "colin.bacon@gmail.com";
            var getUri = string.Format(baseUri, "api/spaceprobe/getdata/{0}");
            var submitUri = String.Format(baseUri, "api/spaceprobe/submitdata/{0}/{1}/{2}");
            var json = string.Empty;

            // Get the directions
            var uri = string.Format(getUri, email);
            json = GetJson(uri);

            var data = JObject.Parse(json)["Directions"];
            var directions = JsonConvert.DeserializeObject<List<string>>(data.ToString());

            IPlotter plotter = new Plotter();

            // Set known (second move) position; row 1, column 0
            plotter.Position = new Coordinate
            {
                x = 0,
                y = 1
            };

            // Work out orienation from first move and known (second move) position
            plotter.CalculateOrientation(directions.First());

            // Remove first move
            directions.RemoveAt(0);

            try
            {
                plotter.PlotMoves(directions);

                Console.WriteLine("End position {0}, {1}", plotter.Position.x, plotter.Position.y);

                // Send position and get results.
                uri = string.Format(submitUri, email, plotter.Position.x, plotter.Position.y);
                json = GetJson(uri);

                var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                Console.WriteLine("Status code: {0}  Message: {1}", result["StatusCode"], result["Message"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Get request for Json data.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        private static string GetJson(string uri)
        {
            var json = string.Empty;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(uri).Result;

                if (response.IsSuccessStatusCode)
                {
                    json = response.Content.ReadAsStringAsync().Result;
                }
            }
            return json;
        }
    }
}
