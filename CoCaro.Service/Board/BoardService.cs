﻿using CoCaro.Data.Models;
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
                    db.Games.Add(
                        new Game()
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
    }
}
