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
       
        //[Route("api/JoinBoard")]
        //[HttpPost]
        //public IActionResult JoinBoard([FromBody] Game game)
        //{
        //    return Ok(_IPlayingService.JoinBoard(game));
        //}
        [Route("api/GetListBoardBlank")]
        [HttpPost]
        public IActionResult GetListBoardBlank()
        {
            return Ok(_IBoardService.GetListBoardValid());
        }
        [HttpPost]
        [Route("api/Board/JoinBoard")]
        public IActionResult JoinBoard([FromBody] Board board)
        {
            var err = new ErrorObject(Error.SUCCESS);
            try
            {
                if (board.Password == null)
                {
                    var result = _IBoardService.CheckBoard(board.Id);
                    if (result.Code == Error.FAILED.Code)
                    {
                        return Ok(result);
                    }                   
                }
                err =_IBoardService.GetBoardByIdAndPass(board);
                return Ok(err);
            }
            catch (Exception ex)
            {
                return (Ok(err.System(ex)));
            }
        }
        [HttpPost]
        [Route("api/Board/CreateBoard")]
        public IActionResult CreateBoard([FromBody] Board board)
        {
            var err = new ErrorObject(Error.SUCCESS);
            try
            {
                board.Owner = _User.Id;
                err = _IBoardService.CreateBoard(board);
                return Ok(err);
            }
            catch (Exception ex)
            {
                return (Ok(err.System(ex)));
            }
        }
    }
}
