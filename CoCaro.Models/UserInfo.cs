using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCaro.Models
{
    public class UserInfo
    {
        public string Username { get; set; }
        public int? Cup { get; set; }
        public double? RateWin { get; set; }
        public int? TotalGame { get; set; }
        public  int Rank { get; set; }
        public string CreateDate { get; set; }
    }
}
