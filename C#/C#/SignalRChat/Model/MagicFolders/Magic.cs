

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
      //Damage = (int)type;
    }

    public void Choose()
    {
      isChoosen = true;
    }
  }
}