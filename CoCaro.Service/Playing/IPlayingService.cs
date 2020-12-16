using CoCaro.Data.Models;
using CoCaro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCaro.Service.Playing
{
    public interface IPlayingService
    {
        ErrorObject JoinBoard(PlayHistory playHistory);
        ErrorObject GetHistoryByUserId(int userid);
    }
}
