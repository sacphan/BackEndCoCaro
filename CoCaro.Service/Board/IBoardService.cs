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
    }
}
