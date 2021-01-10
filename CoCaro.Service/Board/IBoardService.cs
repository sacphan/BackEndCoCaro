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
        ErrorObject GetBoardByIdAndPass(CoCaro.Data.Models.Board board);
        ErrorObject CheckBoard(int id);
    }
}
