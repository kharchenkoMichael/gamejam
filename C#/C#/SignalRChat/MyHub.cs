using Microsoft.AspNet.SignalR;
using SignalRChat.Model;
using SignalRChat.Model.Dto;
using SignalRChat.Model.MagicFolders;
using System.Collections.Generic;
using System.Linq;

namespace SignalRChat
{
  public class MyHub : Hub
  {
    private Dictionary<int, Position> _startPositions =
        new Dictionary<int, Position> { { 1, new Position(1, 1, 1) }, { 2, new Position(2, 2, 2) } };

    public void CreateRoom(string name, int avatarId)
    {
      var firstFreeRoom = GameContext.Instance.Rooms.Where(r => !r.Value.isActive).FirstOrDefault().Value;
     
      var userId = Context.ConnectionId;
      var user = new UserDto(userId, name, new Position(0, 0, 0), firstFreeRoom.Id)
      {
        AvatarId = avatarId        
      };
      
      firstFreeRoom.isActive = true;
      firstFreeRoom.Users.Add(user);
      
      GameContext.Instance.Users.Add(user);
      // Call the broadcastMessage method to update clients.
      GameBroadcast.Instance.UpdateUser();
      GameBroadcast.Instance.UpdateRooms();
    }

    public void JoinToRoom(string name, int avatarId, int roomId)
    {
      var userId = Context.ConnectionId;
      if (!GameContext.Instance.Rooms.ContainsKey(roomId))
        return;

      var user = new UserDto(userId, name, new Position(0, 0, 0), roomId)
      {
        AvatarId = avatarId
      };
      GameContext.Instance.Users.Add(user);

      GameBroadcast.Instance.UpdateUser();
      GameBroadcast.Instance.UpdateRooms();
    }

    public void Update(UserDto user)
    {
      var curUser = GameContext.Instance.Users.Find(item => item.Name == user.Name);
      curUser.Clone(user);
      GameBroadcast.Instance.UpdateUser();
    }
    
    public void GetRoomIds()
    {
      GameBroadcast.Instance.UpdateRooms();
    }

    public void CastMagic(Magic spell)
    {
      GameContext.Instance.AddSpell(spell);
      GameBroadcast.Instance.UpdateSpells();
    }

    public void Test()
    {
      Clients.Client(Context.ConnectionId).message("hello from server");
    }
  }
}