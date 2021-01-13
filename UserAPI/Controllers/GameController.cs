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
                var game = _GameService.GetGameByBoardId(boardId);
                return Ok(err.SetData(game));
            }
            catch (Exception ex)
            {
                return (Ok(err.System(ex)));
            }
        }
        [HttpPost]
        [Route("api/wingame")]
        public IActionResult GetGameByBoardId([FromBody] WinGame winGame)
        {
          
            try
            {
                return  Ok(_GameService.WinGame(winGame.WinnerId,winGame.LoserId,winGame.GameId));
           
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
