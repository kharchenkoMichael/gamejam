using Microsoft.AspNet.SignalR;
using SignalRChat.Model;
using SignalRChat.Model.Dto;
using System.Collections.Generic;

namespace SignalRChat
{
  public class MyHub : Hub
  {
    private Dictionary<int, Position> _startPositions =
        new Dictionary<int, Position> { { 1, new Position(1, 1, 1) }, { 2, new Position(2, 2, 2) } };

    public void Create(UserDto user)
    {
      GameContext.Instance.Users.Add(user);
      // Call the broadcastMessage method to update clients.
      GameBroadcast.Instance.Update();
    }

    public void Update(UserDto user)
    {
      var curUser = GameContext.Instance.Users.Find(item => item.Name == user.Name);
      curUser.Clone(user);
      GameBroadcast.Instance.Update();
    }

    public void Test()
    {
      GameBroadcast.Instance.Broadcast(null);
    }
  }
}