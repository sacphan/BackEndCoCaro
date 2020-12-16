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
        public ErrorObject GetListBoardBlank()
        {
            var err = new ErrorObject(Error.SUCCESS);
            try
            {
                using (var db = new CoCaroContext())
                {
                  
                        var listboardid = db.PlayHistories.Where(p=>p.Result==0 && (p.UserId1==null || p.UserId2==null)).Select(p=>p.BoardId).Distinct().ToList();
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
