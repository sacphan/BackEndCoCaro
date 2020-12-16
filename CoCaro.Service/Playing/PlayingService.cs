using CoCaro.Data.Models;
using CoCaro.Models;
using System;

using System.Linq;


namespace CoCaro.Service.Playing
{
    public class PlayingService : IPlayingService
    {
        public ErrorObject JoinBoard(PlayHistory playHistory)

        {
            var err = new ErrorObject(Error.SUCCESS);
            try
            {
                using (var db = new CoCaroContext())
                {
                    var blankBoard = db.PlayHistories.FirstOrDefault(b => b.BoardId==playHistory.BoardId && (b.UserId1==null || b.UserId2==null));

                    if (blankBoard != null)
                    {
                        err.Failed("bàn đã đầy");
                    }
                    else
                    {
                        db.PlayHistories.Add(playHistory);
                        playHistory.Result = 0; //0:Chưa chơi  1:Thắng 2:Hòa 3:Thua
                        if (playHistory.UserId1==null)
                        {
                            playHistory.UserId1 = playHistory.UserId1;
                        }                            
                        else
                        {
                            playHistory.UserId2 = playHistory.UserId1;
                        }    
                        db.SaveChanges();
                        err.SetData(playHistory);
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
                    var playhistory = db.PlayHistories.Where(b => (b.UserId1 == userid || b.UserId2==userid) && b.Result!=0).ToList();
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
