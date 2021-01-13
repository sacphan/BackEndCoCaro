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
    public class GameServices : IGameService
    {
        public ErrorObject AddGameHistory(GameHistory  gameHistory)
        {
            var error = new ErrorObject(Error.SUCCESS);
            try
            {
                using (var db = new CoCaroContext())
                {
                    var exit = db.GameHistories.FirstOrDefault(x => x.Game == gameHistory.Game && x.PlayerId == gameHistory.PlayerId && x.Turn == gameHistory.Turn);
                    if (exit == null)
                    {
                        db.GameHistories.Add(gameHistory);
                        db.SaveChanges();
                    }                     
                    return error.SetData(gameHistory);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ErrorObject GetListGameById(int UserId)
        {
            var error = Error.Success();
            try
            {
                using (var db = new CoCaroContext())
                {
                    var games = db.Games.Where(x => x.Id == UserId).Include(x => x.Messages).ToList();
                    return error.SetData(games);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public CoCaro.Data.Models.Game GetGameByBoardId(int BoardId)
        {
            CoCaro.Data.Models.Game game = null;
            try
            {
                using (var db = new CoCaroContext())
                {
                    game = db.Games.Where(x => x.BoardId == BoardId &&x.Result==0).Include(x=>x.Board).Include(x=>x.UserId1Navigation).Include(x=>x.UserId2Navigation).Include(x => x.Messages).ToList().FirstOrDefault();
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            return game;
        }
    }
}
