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
        ErrorObject GetGameByBoardId(int BoardId, int UserId);
        ErrorObject AddGameHistory(GameHistory gameHistory);
        ErrorObject GetListChatByGameId(int GameId);
        List<GameHistory> getListGameHistory(int gameid);
    }
}
