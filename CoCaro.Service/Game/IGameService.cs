using CoCaro.Data.Models;
using CoCaro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCaro.Service.Game
{
    public interface IGameService
    {
        ErrorObject GetListGameById(int UserId);
        CoCaro.Data.Models.Game GetGameByBoardId(int BoardId);
        ErrorObject AddGameHistory(GameHistory gameHistory);
        ErrorObject GetListChatByGameId(int GameId);
        List<GameHistory> getListGameHistory(int gameid);
    }
}
