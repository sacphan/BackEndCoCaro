using System;
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
        public static Dictionary<string,string> userOnline = new Dictionary<string, string>();

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (userOnline.Keys.Contains(Context.ConnectionId))
            {
                userOnline.Remove(Context.ConnectionId);
            }
            await Clients.All.SendAsync("offline", userOnline.Values.Distinct().OrderBy(n => n));
            await base.OnDisconnectedAsync(exception);
        }
        public async Task Online(string username)
        {
            if (!string.IsNullOrEmpty(username) && !userOnline.Values.Contains(username))
            {                
                userOnline.Add(Context.ConnectionId,username);                                               
                
            }
            await Clients.All.SendAsync("online", userOnline.Values.Distinct().OrderBy(n => n));
        }
        public async Task Offline(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                if (userOnline.Values.Contains(username))
                {
                    userOnline.Remove(Context.ConnectionId);
                }                
                await Clients.All.SendAsync("offline", userOnline.Values.Distinct().OrderBy(n => n));
            }
        }
    }


}