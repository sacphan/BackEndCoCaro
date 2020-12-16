using CoCaro.Data.Models;
using CoCaro.Service.Board;
using CoCaro.Service.Playing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        private IBoardService _IBoardService;
        private IPlayingService _IPlayingService;

        public BoardController(IBoardService boardService, IPlayingService playingService)
        {
            _IBoardService = boardService;
            _IPlayingService = playingService;
        }
        [Route("api/CreateBoard")]     
        [HttpPost]
        public IActionResult CreateBoard([FromBody] Board board)
        {

            return Ok(_IBoardService.CreateBoard(board));
         
        }
        [Route("api/JoinBoard")]
        [HttpPost]
        public IActionResult JoinBoard([FromBody] PlayHistory playHistory)
        {
            return Ok(_IPlayingService.JoinBoard(playHistory));
        }
    }
}
