using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using AlienAttack.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AlienAttack.Web.Controllers
{
    public class MovesController : ApiController
    {
        const string BaseUri = "http://goserver.cloudapp.net:3000/{0}";
        const string Email = "colin.bacon@gmail.com";

        /// <summary>
        /// The plotter
        /// </summary>
        private readonly IPlotter plotter;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovesController"/> class.
        /// </summary>
        public MovesController()
        {
            this.plotter = new Plotter();
        }

        // GET api/moves
        public IEnumerable<string> Get()
        {
            var getUri = string.Format(BaseUri, "api/spaceprobe/getdata/{0}");
            var submitUri = String.Format(BaseUri, "api/spaceprobe/submitdata/{0}/{1}/{2}");
            var json = string.Empty;

            // Get the directions
            var uri = string.Format(getUri, Email);
            json = GetJson(uri);

            var data = JObject.Parse(json)["Directions"];
            var directions = JsonConvert.DeserializeObject<IEnumerable<string>>(data.ToString());

            return directions;
        }

        // GET api/moves/forward
        public Coordinate Get(Move move)
        {
            this.plotter.Position = position;
            this.plotter.PlotMove(move.NextMove);

            return this.plotter.Position;
        }

        // POST api/moves
        public void Post([FromBody]string value)
        {
        }

        // PUT api/moves/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/moves/5
        public void Delete(int id)
        {
        }
        
        #region Private Methods

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
        #endregion
    }
}
