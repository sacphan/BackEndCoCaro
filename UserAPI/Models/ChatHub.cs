using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace UserAPI.Models
{
    public class ChatHub : Hub
    {
        public static List<string> userOnline = new List<string>();
        public async Task Message(MessageModel message)
        {
            await Clients.Others.SendAsync("message", message);
        }
        public async Task Online(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                if (!userOnline.Contains(username))
                {
                    userOnline.Add(username);
                }                  
                await Clients.All.SendAsync("online", userOnline);
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

    public class MessageModel
    {
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}