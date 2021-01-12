using CoCaro.Data.Models;
using CoCaro.Service.Chat;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Models
{
    public class GameHub:Hub
    {
        private IChatService _ICharService;
        public GameHub(IChatService chatService)
        {
            _ICharService = chatService;
        }
        public async Task Message(Message message)
        {
            if (!string.IsNullOrEmpty(message.Message1))
            {
                _ICharService.SendMessage(message.UserId.Value, message.GameId.Value, message.Message1);
                await Clients.Others.SendAsync("message", message.Message1);
            }

        }
    }
}
