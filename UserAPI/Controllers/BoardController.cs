using CoCaro.Data.Models;
using CoCaro.Models;
using CoCaro.Service.Board;
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

        public BoardController(IBoardService boardService)
        {
            _IBoardService = boardService;
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
                err =_IBoardService.JoinBoard(board, _User.Id);
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

        [Route("api/Board/JoinBoardNow")]
        public IActionResult JoinBoardNow()
        {
            var err = new ErrorObject(Error.SUCCESS);
            try
            {               
                err = _IBoardService.JoinBoardNow(_User.Id);
                return Ok(err);
            }
            catch (Exception ex)
            {
                return (Ok(err.System(ex)));
            }
        }
    }
}
