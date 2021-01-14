using CoCaro.Models;
using CoCaro.Service.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Controllers
{
  
    public class GameController : BaseController
    {
        private IGameService _GameService;
        public GameController(IGameService gameService)
        {
            _GameService = gameService;
        }
        [HttpPost]
        [Route("api/GetGameByBoardId")]
        public IActionResult GetGameByBoardId([FromBody] int boardId)
        {
            var err = new ErrorObject(Error.SUCCESS);
            try
            {
                err = _GameService.GetGameByBoardId(boardId, _User.Id);
                if (err.Code == Error.SUCCESS.Code)
                {                
                    return Ok(err);                    
                }           
                else
                {
                    return Ok(err);
                }
            }
            catch (Exception ex)
            {
                return (Ok(err.System(ex)));
            }
        }
        [HttpGet]
        [Route("api/GetListGameHistory")]
        [Authorize]
        public IActionResult GetListGameHistory()
        {
            var error = new ErrorObject(Error.SUCCESS);
            try
            {
                error = _GameService.GetListGameById(_User.Id);
                return Ok(error);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
