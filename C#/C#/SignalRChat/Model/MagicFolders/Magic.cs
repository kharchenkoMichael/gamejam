

namespace SignalRChat.Model.MagicFolders
{
  public class Magic
  {
    public string TargetName;
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