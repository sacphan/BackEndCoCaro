using CoCaro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCaro.Service.Chat
{
    public interface IChatService
    {
        ErrorObject LoadMessageByBoardId(int boardId);
    
        ErrorObject SendMessage(int userId, int gameId, string message, int turn);
    }
}
