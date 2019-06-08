using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.Model.MagicFolders
{
  public class Magic
  {
    public MagicType Type;
    public string Name;
    public string Description;
    public int Damage;
    public bool isChoosen;

    public Magic(string name, MagicType type, string description)
    {
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