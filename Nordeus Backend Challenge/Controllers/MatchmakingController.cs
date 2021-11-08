using Common.Models;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nordeus_Backend_Challenge.Controllers
{
    [Route("api/matchmaking")]
    [ApiController]
    public class MatchmakingController : ControllerBase
    {
        private readonly IMatchmakingService _matchmakingService;

        public MatchmakingController(IMatchmakingService matchmakingService)
        {
            _matchmakingService = matchmakingService;
        }

        /// Implementation is done with assumption that all users are logged in.
        [HttpPost]
        public ObjectResult Post(List<int> clubsIdsPool)
        {
            try
            {
                List<MatchmakingResult> resultOfMatchmaking = _matchmakingService.DoMatchmaking(clubsIdsPool);
                return Ok(resultOfMatchmaking);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
