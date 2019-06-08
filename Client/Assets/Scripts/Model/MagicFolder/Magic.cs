using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.MagicFolder
{
  public class Magic
  {
    public int Id { get; }
    public MagicType Type { get; }
    public string Name { get; }
    public string Description { get; }
    public int Damage;
    public bool isChoosen;

    public Magic(int id, string name, MagicType type, string description)
    {
      Id = id;
      Name = name;
      Type = type;
      Description = description;
      isChoosen = false;
      //damage
    }

    public void Choose()
    {
      isChoosen = true;
    }
  }
}
