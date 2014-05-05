using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AlienAttack.Web.Proxy;

namespace AlienAttack.Web.Controllers
{
    /// <summary>
    /// The moves api controller.
    /// </summary>
    [RoutePrefix("api/moves")]
    public class MovesController : ApiController
    {
        /// <summary>
        /// The plotter.
        /// </summary>
        private readonly IPlotter plotter;

        /// <summary>
        /// The space probe proxy.
        /// </summary>
        private readonly ISpaceProbeProxy spaceProbeProxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovesController"/> class.
        /// </summary>
        public MovesController()
        {
            this.spaceProbeProxy = new SpaceProbeProxy();
            this.plotter = new Plotter();
        }

        /// <summary>
        /// Gets the moves.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="currentPosition">The current position.</param>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IEnumerable<Position> Get([FromUri]string email, [FromUri]Coordinate currentPosition)
        {
            var directions = this.spaceProbeProxy.GetData(email).ToList();
            var firstMove = directions.FirstOrDefault();

            this.plotter.Position = currentPosition;
            plotter.CalculateOrientation(firstMove);

            directions.Remove(firstMove);

            var positions = this.plotter.PlotMoves(directions);

            return positions;
        }

        /// <summary>
        /// Submits the data.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        [Route("position/submit")]
        [HttpGet]
        public string SubmitData([FromUri]string email, [FromUri]Coordinate position)
        {
            var message = this.spaceProbeProxy.SubmitData(email, position);

            return message;
        }
    }
}
