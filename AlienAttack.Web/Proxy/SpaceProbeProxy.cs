using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AlienAttack.Web.Proxy
{
    /// <summary>
    /// The space probe proxy.
    /// </summary>
    public class SpaceProbeProxy : ISpaceProbeProxy
    {
        const string BaseUri = "http://goserver.cloudapp.net:3000/{0}";
        readonly string getDataUri = string.Format(BaseUri, "api/spaceprobe/getdata/{0}");
        readonly string submitDataUri = string.Format(BaseUri, "api/spaceprobe/submitdata/{0}/{1}/{2}");

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public IEnumerable<string> GetData(string email)
        {
            var uri = string.Format(getDataUri, email);
            var json = GetJson(uri);
            var data = JObject.Parse(json)["Directions"];
            var directions = JsonConvert.DeserializeObject<IEnumerable<string>>(data.ToString());

            return directions;
        }

        /// <summary>
        /// Submits the data.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public string SubmitData(string email, Coordinate position)
        {
            var uri = string.Format(submitDataUri, email, position.x, position.y);
            var json = GetJson(uri);
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            return result["Message"];
        }

        #region Private Methods

        /// <summary>
        /// Gets the json.
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
        #endregion
    }
}