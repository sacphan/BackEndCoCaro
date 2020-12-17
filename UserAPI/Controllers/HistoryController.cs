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
    public class HistoryController : BaseController
    {
        private IPlayingService _IPlayingService;
        public HistoryController(IPlayingService playingService)
        {
            _IPlayingService = playingService;
        }
        [Route("api/GetHistoryByUserId")]
        [HttpPost]
        public IActionResult GetHistoryByUserId([FromBody] int userId)
        {
            return Ok(_IPlayingService.GetHistoryByUserId(userId));
        }
    }
}
