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
                    var exit = db.GameHistories.FirstOrDefault(x => x.GameId == gameHistory.GameId && x.PlayerId == gameHistory.PlayerId && x.Turn == gameHistory.Turn);
                    if (exit == null)
                    {
                        db.GameHistories.Add(gameHistory);
                        db.SaveChanges();
                        return error.SetData(gameHistory);
                    }        
                    else
                    {
                        return error.SetData(exit);
                    }    
                   
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
                    var games = db.Games.Where(x => x.Result != 0 && (x.UserId1 == UserId || x.UserId2 == UserId)).Include(x => x.UserId1Navigation).Include(x => x.UserId2Navigation).ToList();
                    for (int i = 0; i < games.Count; i++)
                    {
                        games[i].UserId1Navigation.GameUserId1Navigations = null;
                        games[i].UserId1Navigation.GameUserId2Navigations = null;
                        games[i].UserId2Navigation.GameUserId2Navigations = null;
                        games[i].UserId2Navigation.GameUserId1Navigations = null;

                    }
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
        public ErrorObject GetListChatByGameId(int GameId)
        {
            var error = Error.Success();
            try
            {
                using (var db = new CoCaroContext())
                {
                    var games = db.Games.Where(x => x.Id == GameId).Include(x =>x.Messages).Include(x=>x.UserId1Navigation).Include(x=>x.UserId2Navigation).FirstOrDefault();
                    return error.SetData(games.Messages);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
