using CoCaro.Data.Models;
using CoCaro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCaro.Service.Chat
{
    public class ChatService: IChatService
    {
        public ErrorObject SendMessage(int userId,int boardId,string message)
        {
            var err = new ErrorObject(Error.SUCCESS);
            try
            {
                using (var db = new CoCaroContext())
                {
                    var messageSend = new Message()
                    {
                        Message1 = message,
                        UserId = userId,
                        BoardId = boardId
                    };
                    db.Messages.Add(messageSend);
                 
                    db.SaveChanges();
                    err.SetData(messageSend);

                }
            }
            catch (Exception ex)
            {

                return err.Failed(ex.Message);
            }
            return err;
        }
        public ErrorObject LoadMessageByBoardId(int boardId)
        {
            var err = new ErrorObject(Error.SUCCESS);
            try
            {
                using (var db = new CoCaroContext())
                {              
                    var listMes = db.Messages.Where(m=>m.BoardId == boardId).ToList();
                    err.SetData(listMes);
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
