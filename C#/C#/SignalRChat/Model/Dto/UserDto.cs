using System.Collections.Generic;
using SignalRChat.Model.Spells;

namespace SignalRChat.Model.Dto
{  
  public class UserDto
  {
    public string Id;
    public string Name;
    public int AvatarId;
    public int Hp;
    public int Mana;
    public int Speed;
    public Position Position;
    public List<int> Magic;
    public List<ISpellPosteffect> Posteffects;

    public int RoomId;
    //public bool isDead { get; set; }

    public UserDto(string id, string name, Position position, int roomNumber)
    {
      Id = id;
      Name = name;
      Position = position;
      RoomId = roomNumber;
      Magic = new List<int>();
      Posteffects = new List<ISpellPosteffect>();
      Mana = 100;
      Hp = 100;
      //set default hp and mana and speed
    }

    public void Clone(UserDto user)
    {
      this.Position = user.Position;
      this.Magic = user.Magic;
      Hp = user.Hp;
      Mana = user.Mana;
      Speed = user.Speed;
    }
  }
}