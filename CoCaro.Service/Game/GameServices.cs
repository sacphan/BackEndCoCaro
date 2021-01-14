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
        public List<GameHistory> getListGameHistory(int gameid)
        {
            using (var db = new CoCaroContext())
            {
                var gameHistories = db.GameHistories.Where(x => x.GameId == gameid).ToList();
                return gameHistories;
            }
        }
        public ErrorObject AddGameHistory(GameHistory gameHistory)
        {
            var error = new ErrorObject(Error.SUCCESS);
            try
            {
                using (var db = new CoCaroContext())
                {
                    var exit = db.GameHistories.FirstOrDefault(x => x.GameId == gameHistory.GameId && x.PlayerId == gameHistory.PlayerId && x.Turn == gameHistory.Turn && x.Postion == gameHistory.Postion);
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
        public ErrorObject WinGame(int WinnerId,int LoserId,int GameId)
        {
            var error = new ErrorObject(Error.SUCCESS);
            try
            {
                using (var db = new CoCaroContext())
                {
                    var game = db.Games.FirstOrDefault(game => game.Id == GameId);
                    game.Result = WinnerId;
                    var userWinner = db.Users.FirstOrDefault(u => u.Id == WinnerId);
                    userWinner.Cup = userWinner.Cup == null ? 0 : userWinner.Cup +10;
                    userWinner.TotalGame = (userWinner.TotalGame ?? 0) + 1;
                    var userLoser = db.Users.FirstOrDefault(u => u.Id == LoserId);
                    userLoser.Cup = userLoser.Cup==null ? 0: userLoser.Cup-5;
                    userLoser.TotalGame = (userWinner.TotalGame ?? 0) + 1;
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return error;
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

            public ErrorObject GetGameHistoryByGameId(int gameId)
            {
                var error = Error.Success();
                try
                {
                    using (var db = new CoCaroContext())
                    {
                        var game = db.Games.Where(x => x.Id == gameId).Include(x => x.GameHistories).Include(x => x.Messages).ToList().FirstOrDefault();
                        return error.SetData(game);
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
            public ErrorObject GetGameByBoardId(int BoardId, int UserId)
        {
            var error = Error.Success();
            try
            {
                using (var db = new CoCaroContext())
                {
                    var game = db.Games.Where(x => x.BoardId == BoardId && x.Result == 0).Include(x => x.Board).Include(x => x.UserId1Navigation).Include(x => x.UserId2Navigation).Include(x => x.Messages).ToList().FirstOrDefault();
                    var b = db.Boards.FirstOrDefault(x => x.Id == game.BoardId);
                    if (b.Password != null && b.Password != "")
                    {
                        if (game.UserId1 != UserId && game.UserId2 != UserId)
                        {
                            return error.Failed("Bạn đã đi lạc rồi");

                        }
                        else
                        {
                            return error.SetData(game);
                        }
                    }
                    else
                    {
                        if (game.UserId1 != null && (game.UserId1 == UserId || game.UserId2 == UserId))
                        {
                            return error.SetData(game);
                        }

                        if (game.UserId2 == null && game.UserId1 != null)
                        {
                            game.UserId2 = UserId;
                            b.Status = 2;
                            db.SaveChanges();
                        }
                        else
                        if (game.UserId1 == null && game.UserId2 != null)
                        {
                            game.UserId1 = UserId;
                            b.Status = 2;
                            db.SaveChanges();
                        }
                        return error.SetData(game);

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ErrorObject GetListChatByGameId(int GameId)
        {
            var error = Error.Success();
            try
            {
                using (var db = new CoCaroContext())
                {
                    var games = db.Games.Where(x => x.Id == GameId).Include(x => x.Messages).Include(x => x.UserId1Navigation).Include(x => x.UserId2Navigation).FirstOrDefault();
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
