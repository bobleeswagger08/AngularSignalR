using my_app_chat.Models;  
using Microsoft.AspNetCore.SignalR;  
using System.Threading.Tasks; 

namespace my_app_chat.Hubs  
{  
    public class MessageHub : Hub  
    {  
        public async Task NewMessage(Message msg)  
        {  
            await Clients.All.SendAsync("MessageReceived", msg);  
        }  
    }  
}  