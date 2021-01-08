using CoCaro.Data.Models;
using CoCaro.Models;
using System;

using System.Linq;


namespace CoCaro.Service.Playing
{
    public class PlayingService : IPlayingService
    {
        public ErrorObject JoinBoard(Game game)

        {
            var err = new ErrorObject(Error.SUCCESS);
            try
            {
                using (var db = new CoCaroContext())
                {
                    var blankBoard = db.Games.FirstOrDefault(b => b.BoardId==game.BoardId && (b.UserId1==null || b.UserId2==null));

                    if (blankBoard != null)
                    {
                        err.Failed("bàn đã đầy");
                    }
                    else
                    {
                        db.Games.Add(game);
                        game.Result = 0; //0:Chưa chơi  1:Thắng 2:Hòa 3:Thua
                        if (game.UserId1==null)
                        {
                            game.UserId1 = game.UserId1;
                        }                            
                        else
                        {
                            game.UserId2 = game.UserId1;
                        }    
                        db.SaveChanges();
                        err.SetData(game);
                    }

                }
            }
            catch (Exception ex)
            {

                return err.Failed(ex.Message);
            }
            return err;
        }
        public ErrorObject GetHistoryByUserId(int userid)

        {
            var err = new ErrorObject(Error.SUCCESS);
            try
            {
                using (var db = new CoCaroContext())
                {
                    var playhistory = db.Games.Where(b => (b.UserId1 == userid || b.UserId2==userid) && b.Result!=0).ToList();
                    err.SetData(playhistory);
                 

                }
            }
            catch (Exception ex)
            {

                return err.Failed(ex.Message);
            }
            return err;
        }
    }
}
