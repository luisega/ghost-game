using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace GhostGame.Controllers
{
    [Route("api/[controller]")]
    public class GhostGameController : Controller
    {
        private readonly ILogger _logger;
        private readonly IWordTreeManager _wordTreeManager;

        public GhostGameController(IWordTreeManager wordTreeManager)
        {
            _wordTreeManager = wordTreeManager;
            //_logger = logger;
        }

        [HttpPost]
        public ActionResult ProcessPlayerMovement([FromBody]GhostGameRequestDto requestDto)
        {
            try
            {
                return Ok(_wordTreeManager.GetNextMovement(requestDto.CurrentWord));
            }
            catch (Exception e)
            {
                return CreateAndLogInternalServerError(e);
            }
        }

        [HttpPost("reset")]
        public ActionResult ResetGame()
        {
            try
            {
                return Ok(_wordTreeManager.ResetGame());
            }
            catch (Exception e)
            {
                return CreateAndLogInternalServerError(e);
            }
        }

        protected internal ObjectResult CreateAndLogInternalServerError(Exception exception)
        {
            string msg = exception.Message;
            //_logger.LogError(msg);
            return StatusCode(500, msg);
        }
    }
}
