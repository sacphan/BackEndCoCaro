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
                    var exitboard = db.Boards.FirstOrDefault(b => b.Name.ToLower().Equals(board.Name.ToLower()));
                    if (exitboard!=null)
                    {
                        err.Failed("bàn đã tồn tại");
                    } 
                    else
                    {
                        db.Boards.Add(board);
                        db.SaveChanges();
                        err.SetData(board);
                    }    
                  
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
