using CoCaro.Data.Models;
using CoCaro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCaro.Service.Board
{
    public interface IBoardService
    {
        ErrorObject CreateBoard(CoCaro.Data.Models.Board board);
        ErrorObject GetListBoardValid();
        CoCaro.Data.Models.Board GetBoardBlank(User user);
        ErrorObject JoinBoard(CoCaro.Data.Models.Board board, int UserId2);
        ErrorObject CheckBoard(int id);
        ErrorObject JoinBoardNow(int UserId2);

    }
}
