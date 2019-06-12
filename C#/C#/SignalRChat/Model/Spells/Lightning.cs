using SignalRChat.Model.Dto;
using SignalRChat.Model.MagicFolders;

namespace SignalRChat.Model.Spells
{
  public class LightningPosteffect : ISpellPosteffect
  {
    public MagicType Type { get { return MagicType.Lightning; } }
    public bool isActive { get; set; }

    public void Start(UserDto user)
    {
      isActive = true;
     //todo: change move axis;
    }

    public void Update(UserDto user, double deltaTimeSec)
    {
    }
  }
}
