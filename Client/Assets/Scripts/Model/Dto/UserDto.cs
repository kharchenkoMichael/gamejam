using System.Collections.Generic;
using Assets.Scripts.Model.Entities;
using Assets.Scripts.Model.MagicFolder;
using Assets.Scripts.Model.Spells;

namespace Model.Dto
{ 

  public class UserDto
  {
    public string Id { get; }
    public string Name { get; }
    public int AvatarId { get; }
    public int Hp { get; set; }
    public int Mana { get; set; }
    public int Speed { get; set; }
    public Position Position { get; set; }
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
      //set default hp and mana and speed
    }

    public void Clone(UserDto user)
    {
      this.Position = user.Position;
    }

    public void Attack(int Damage)
    {

    }

  }
}