using CoCaro.Data.Models;
using CoCaro.Models;
using System;
using System.Linq;

namespace CoCaro.Service.Board
{
    public class BoardService : IBoardService
    {
        public ErrorObject CreateBoard(CoCaro.Data.Models.Board board)
        {
            var err = new ErrorObject(Error.SUCCESS);
            try
            {
                using (var db = new CoCaroContext())
                {
                    board.Status = 1;
                    db.Boards.Add(board);
                    db.SaveChanges();
                    return err.SetData(board);
                }
            }
            catch (Exception ex)
            {

                return err.Failed(ex.Message);
            }
        }
        public CoCaro.Data.Models.Board GetBoardBlank(User user)
        {
            CoCaro.Data.Models.Board board = null  ;
            try
            {
                using (var db = new CoCaroContext())
                {
                    var playhistory = db.Games.FirstOrDefault(p => p.Result == 0 && p.UserId1 == null && p.UserId2 == null);
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
        public ErrorObject GetListBoardValid()
        {
            var err = new ErrorObject(Error.SUCCESS);
            try
            {
                using (var db = new CoCaroContext())
                {
                    var data = db.Boards.Where(b => b.Status >0).ToList();
                    err.SetData(data);
                }
            }
            catch (Exception ex)
            {

                return err.Failed(ex.Message);
            }
            return err;
        }
        public ErrorObject GetBoardByIdAndPass(CoCaro.Data.Models.Board board)
        {
            var error = Error.Success();
            try
            {
                using (var db = new CoCaroContext())
                {
                    var b = db.Boards.Where(b => b.Id ==board.Id).FirstOrDefault();
                    if (b.Password == null|| b.Password=="" || b.Password == board.Password)
                    {
                        return error.SetData(b);
                    }
                }
                return error.Failed("Wrong Password");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ErrorObject CheckBoard(int id)
        {
            var error = Error.Success();

            try
            {
                using (var db = new CoCaroContext())
                {
                    var b = db.Boards.FirstOrDefault(b => b.Id ==id);
                    if (b == null || b.Status == 0)
                    {
                        return error.Failed("Bàn chơi không tồn tại");
                    } 
                    if (b.Status == 2)
                    {
                        return error.Failed("Bàn chơi đã đầy");
                    }
                    if (b.Password != null && b.Password.Length> 0)
                    {
                        return error.Failed("Has password").SetData(b);
                    }
                }
                return error;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
