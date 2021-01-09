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
        private IChatService _ICharService;
        public ChatHub(IChatService chatService)
        {
            _ICharService = chatService;
        }
        public async Task Message(Message message)
        {
            if (!string.IsNullOrEmpty(message.Message1))
            {
                _ICharService.SendMessage(message.UserId.Value,message.BoardId.Value,message.Message1);
                await Clients.Others.SendAsync("message", message.Message1);
            }
           
        }
        public async Task Online(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                if (!userOnline.Contains(username))
                {
                    userOnline.Add(username);
                }                  
                
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