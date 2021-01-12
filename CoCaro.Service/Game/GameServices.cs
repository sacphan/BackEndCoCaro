using CoCaro.Data.Models;
using CoCaro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCaro.Service.Game
{
    public class GameServices : IGameService
    {
        public ErrorObject GetListGameById(int UserId)
        {
            var error = Error.Success();
            try
            {
                using (var db = new CoCaroContext())
                {
                    var games = db.Games.Where(x => x.Id == UserId).Include(x => x.Messages).ToList();
                    return error.SetData(games);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
