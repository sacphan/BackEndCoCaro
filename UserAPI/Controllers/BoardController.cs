using CoCaro.Data.Models;
using CoCaro.Models;
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
    public class BoardController : BaseController
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
        public IActionResult CreateBoard()
        {
            var err = new ErrorObject(Error.SUCCESS);
            try
            {
                var checkblank = _IBoardService.GetBoardBlank(_User);
                if (checkblank == null)
                {
                    err = _IBoardService.CreateBoard(_User);
                }
                else err.SetData(checkblank);
                return Ok(err);
            }
            catch (Exception ex)
            {

                return Ok(err.Failed(ex.Message));
            }
            
         
        }
        [Route("api/JoinBoard")]
        [HttpPost]
        public IActionResult JoinBoard([FromBody] PlayHistory playHistory)
        {
            return Ok(_IPlayingService.JoinBoard(playHistory));
        }
        [Route("api/GetListBoardBlank")]
        [HttpPost]
        public IActionResult GetListBoardBlank()
        {
            return Ok(_IBoardService.GetListBoardBlank());
        }
    }
}
