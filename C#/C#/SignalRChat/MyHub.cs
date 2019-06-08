using Microsoft.AspNet.SignalR;
using SignalRChat.Model;

namespace SignalRChat
{
    public class MyHub : Hub
    {
        
        public void Create(string name)
        {
            var user = new User(name);
            GameContext.Instance.Users.Add(user);
            // Call the broadcastMessage method to update clients.
            GameBroadcast.Instance.Update();
        }

        public void Update(User user)
        {
            var curUser = GameContext.Instance.Users.Find(item => item.Name == user.Name);
            curUser.Clone(user);
            GameBroadcast.Instance.Update();
        }
    }
}