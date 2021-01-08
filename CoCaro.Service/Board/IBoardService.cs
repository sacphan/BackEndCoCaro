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
        ErrorObject CreateBoard(User user);
        ErrorObject GetListBoardBlank();
        CoCaro.Data.Models.Board GetBoardBlank(User user);
    }
}
