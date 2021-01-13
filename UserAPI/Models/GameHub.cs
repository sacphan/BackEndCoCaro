using CoCaro.Data.Models;
using CoCaro.Service.Chat;
using CoCaro.Service.Game;
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
        private IGameService _IGameService;
        public static List<GameHistory> ListGameHistories = new List<GameHistory>();
        public GameHub(IChatService chatService, IGameService gameService)
        {
            _ICharService = chatService;
            _IGameService = gameService;

        }
        public async Task Message(Message message)
        {
            if (!string.IsNullOrEmpty(message.Message1))
            {
                _ICharService.SendMessage(message.UserId.Value, message.GameId.Value, message.Message1,message.Turn??0);
                await Clients.Others.SendAsync("message"+ message.Turn, message);
            }

        }
        
        public async Task Game(Game game)
        {   
                await Clients.Others.SendAsync("game", game);          
        }
       
        public async Task Ready(string username)
        {           
                //var idConnection = ChatHub.userOnline.FirstOrDefault(v => v.Value == username).Key;
                await Clients.Others.SendAsync("ready", true);          
        }
        public async Task Start()
        {
            //var idConnection = ChatHub.userOnline.FirstOrDefault(v => v.Value == username).Key;
            await Clients.Others.SendAsync("start", true);
        }
        public async Task Play(GameHistory gameHistory)
        {
            _IGameService.AddGameHistory(gameHistory);
            await Clients.Others.SendAsync("play"+gameHistory.Turn, gameHistory);
        }
    }
}
