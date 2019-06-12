using SignalRChat.Model.Dto;
using SignalRChat.Model.MagicFolders;

namespace SignalRChat.Model.Spells
{
  public class StonefallPosteffect : ISpellPosteffect
  {
    public MagicType Type { get { return MagicType.Stonefall; } }
    public bool isActive { get; set; }

    private double _timer = 2;
    private int _oldSpeed;

    public void Start(UserDto user)
    {
      isActive = true;
      _oldSpeed = user.Speed;
      user.Speed = 0;
    }

    public void Update(UserDto user, double deltaTimeSec)
    {
      _timer -= deltaTimeSec;
      if (_timer <= 0)
      {
        user.Speed = _oldSpeed;
      }
    }
  }
}
