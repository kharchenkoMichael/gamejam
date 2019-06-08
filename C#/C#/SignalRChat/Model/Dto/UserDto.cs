using System.Collections.Generic;
using SignalRChat.Model.MagicFolders;

namespace SignalRChat.Model.Dto
{
  public class UserDto
  {
    public string Id { get; }
    public string Name { get; }
    public int AvatarId { get; set; }
    public int Hp { get; set; }
    public int Mana { get; set; }
    public int Speed { get; set; }
    public Position Position { get; set; }
    public List<int> Magic { get; set; }
    public int RoomName { get; }
    //public bool isDead { get; set; }

    public UserDto(string id, string name, Position position, int roomNumber)
    {
      Name = name;
      Position = position;
      RoomName = roomNumber;
      //set default hp and mana and speed
    }

    public void Clone(UserDto user)
    {
      this.Position = user.Position;
    }
  }
}