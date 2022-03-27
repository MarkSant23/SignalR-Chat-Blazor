using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Server.Hubs
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> Users = new Dictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            string uname = Context.GetHttpContext().Request.Query["username"];
            Users.Add(Context.ConnectionId, uname);
            await AddMessageToChat(string.Empty, $"{uname} is connected.");
            await base.OnConnectedAsync();

        }

        public override async Task OnDisconnectedAsync(Exception? ex)
        {
            string uname = Users.FirstOrDefault(i => i.Key == Context.ConnectionId).Value;
            await AddMessageToChat(string.Empty, $"{uname} is out.");
        }

        public async Task AddMessageToChat(string user, string message)
        {
            await Clients.All.SendAsync("GetMessage", user, message);
        
        }
    }
}
