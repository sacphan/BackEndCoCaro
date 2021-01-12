using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoCaro.Data.Models;
using CoCaro.Service.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace UserAPI.Models
{
    //[Authorize]
    public class ChatHub : Hub
    {
        public static List<string> userOnline = new List<string>();
     
        public async Task Online(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {                
                userOnline.Add(username);                                               
                await Clients.All.SendAsync("online", userOnline.Distinct().OrderBy(n=>n));
            }    
           
        }
        public async Task Offline(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                if (userOnline.Contains(username))
                {
                    userOnline.Remove(username);
                }                
                await Clients.All.SendAsync("offline", userOnline);
            }
        }
    }


}