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
                    var b = db.Boards.FirstOrDefault(x => x.Status == 0 || x.Status ==null);
                    if (b!= null)
                    {
                        b.Status = 1;
                        b.Password = board.Password;
                        b.Owner = board.Owner;
                        b.TimeOfTurn = board.TimeOfTurn;
                        board.Id = b.Id;
                        db.SaveChanges();
                    }
                    else
                    {
                        board.Status = 1;
                        db.Boards.Add(board);
                        db.SaveChanges();
                    }
                    err.SetData(board);
                    var game = db.Games.FirstOrDefault(x => x.BoardId == board.Id &&  x.Result == 0);
                    if (game != null)
                    {
                        game.UserId1 = board.Owner;
                    }
                    else
                    {
                        game = new CoCaro.Data.Models.Game();
                        game.UserId1 = board.Owner;
                        game.BoardId = board.Id;
                        db.Add(game);
                    }
                    db.SaveChanges();
                    return err;
                }
            }
            catch (Exception ex)
            {

                return err.Failed(ex.Message);
            }
        }
        public CoCaro.Data.Models.Board GetBoardBlank(User user)
        {
            CoCaro.Data.Models.Board board = null;
            try
            {
                using (var db = new CoCaroContext())
                {
                    var playhistory = db.Games.FirstOrDefault(p => p.Result == 0 && p.UserId1 == null && p.UserId2 == null);
                    if (playhistory != null)
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
                    var data = db.Boards.Where(b => b.Status > 0).ToList();
                    err.SetData(data);
                }
            }
            catch (Exception ex)
            {

                return err.Failed(ex.Message);
            }
            return err;
        }
        public ErrorObject JoinBoard(CoCaro.Data.Models.Board board, int UserId2) // join board
        {
            var error = Error.Success();
            try
            {
                using (var db = new CoCaroContext())
                {
                    var b = db.Boards.Where(b => b.Id == board.Id).FirstOrDefault();
                    if (b.Password == null || b.Password == "" || b.Password == board.Password)
                    {
                        var game = db.Games.FirstOrDefault(x => x.BoardId == board.Id && x.Result == 0);
                        if (game != null)
                        {
                            b.Status = 2;
                            game.UserId2 = UserId2;
                            db.SaveChanges();
                        }
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
                    var b = db.Boards.FirstOrDefault(b => b.Id == id);
                    if (b == null || b.Status == 0)
                    {
                        return error.Failed("Bàn chơi không tồn tại");
                    }
                    if (b.Status == 2)
                    {
                        return error.Failed("Bàn chơi đã đầy");
                    }
                    if (b.Password != null && b.Password.Length > 0)
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
        public ErrorObject JoinBoardNow(int UserId2)
        {
            var error = Error.Success();
            try
            {
                using (var db = new CoCaroContext())
                {
                    var b = db.Boards.Where(b => b.Status == 1 && (b.Password == null || b.Password== "")).FirstOrDefault(); 
                    if (b != null)
                    {
                        b.Status = 2;
                        var game = db.Games.FirstOrDefault(x => x.BoardId == b.Id && x.Result == 0);
                        if (game != null)
                        {
                            game.UserId2 = UserId2;
                        }
                        db.SaveChanges();
                        return error.SetData(b);
                    }
                    return error.Failed("Không có phòng trống");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
