using CoCaro.Data.Models;
using CoCaro.Models;
using System;
using System.Linq;

namespace CoCaro.Service.Board
{
    public class BoardService : IBoardService
    {
        public ErrorObject CreateBoard(User user)
        {
            var err = new ErrorObject(Error.SUCCESS);
            try
            {
                using (var db = new CoCaroContext())
                {
                    var board = new CoCaro.Data.Models.Board();
                    db.Boards.Add(board);
                    db.SaveChanges();
                    board.Name = board.Id.ToString();
                    db.PlayHistories.Add(
                        new PlayHistory()
                        {
                            BoardId = board.Id,
                            UserId1 = user.Id
                        }) ;
                    db.SaveChanges();
                    err.SetData(board);
                }
            }
            catch (Exception ex)
            {

                return err.Failed(ex.Message);
            }
            return err;
        }
        public CoCaro.Data.Models.Board GetBoardBlank(User user)
        {
            CoCaro.Data.Models.Board board = null  ;
            try
            {
                using (var db = new CoCaroContext())
                {
                    var playhistory = db.PlayHistories.FirstOrDefault(p => p.Result == 0 && p.UserId1 == null && p.UserId2 == null);
                    if (playhistory!=null)
                    {
                        playhistory.UserId1 = user.Id;
                        board = db.Boards.FirstOrDefault(b => b.Id == playhistory.BoardId);
                    }    
                         
                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return board;
        }
        public ErrorObject GetListBoardBlank()
        {
            var err = new ErrorObject(Error.SUCCESS);
            try
            {
                using (var db = new CoCaroContext())
                {

                    var listboardid = db.PlayHistories.Where(p => p.Result == 0 && (p.UserId1 == null || p.UserId2 == null)).Select(p => p.BoardId).Distinct().ToList();
                    var data = db.Boards.Where(b => listboardid.Contains(b.Id)).ToList();
                    err.SetData(data);
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
