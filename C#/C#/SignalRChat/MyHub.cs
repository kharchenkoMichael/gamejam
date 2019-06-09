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
        new Dictionary<int, Position> { { 1, new Position(7, 1, 0) }, { 2, new Position(-7, 1, 0) } };

    public void CreateRoom(string name, int avatarId)
    {
      var firstFreeRoom = GameContext.Instance.Rooms.Where(r => !r.Value.isActive).FirstOrDefault().Value;
     
      var userId = Context.ConnectionId;
      var user = new UserDto(userId, name, new Position(0, 0, 0), firstFreeRoom.Id)
      {
        AvatarId = avatarId        
      };
      
      firstFreeRoom.isActive = true;
      firstFreeRoom.Users.Add(user.Name);
      
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

      if (GameContext.Instance.Rooms.ContainsKey(roomId))
      {
        var room = GameContext.Instance.Rooms[roomId];
        room.Users.Add(user.Name);
      }
      //todo: if key was not found
      
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

    public void CastMagic(SpellDto spell)
    {
      GameContext.Instance.AddSpell(spell);
      GameBroadcast.Instance.UpdateSpells();
    }

    public void UserExit(string userName)
    {
      var user = GameContext.Instance.Users.Where(u => u.Name == userName).FirstOrDefault();
      if (user == null)
        return;

      var room = GameContext.Instance.Rooms[user.RoomId];
      foreach (var roomUser in room.Users)
      {
        var delUser = GameContext.Instance.Users.Find(item => item.Name == roomUser);
        GameContext.Instance.Users.Remove(delUser);
      }

      room.Users.Clear();
      room.isActive = false;

      Clients.All.quit();
    }

    public void Test()
    {
      Clients.Client(Context.ConnectionId).message("hello from server");
    }

    public void StartGame(List<UserDto> users)
    {
      foreach (var user in users)
      {
        var room = GameContext.Instance.Rooms[user.RoomId];
        if (room.Users[0] == user.Name)
        {
          user.Position = _startPositions[1];
        }
        else
        {
          user.Position = _startPositions[2];
        }
        var curUser = GameContext.Instance.Users.Find(item => item.Name == user.Name);
        curUser.Clone(user);
      }
      GameBroadcast.Instance.StartGame();
    }
  }
}