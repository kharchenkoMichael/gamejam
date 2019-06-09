using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalRChat.Model.Dto;
using SignalRChat.Model.MagicFolders;

namespace Assets.Scripts.Model.Spells
{
  public class FireBallPosteffect : ISpellPosteffect
  {
    public MagicType Type { get { return MagicType.Fireball; } }
    public bool isActive { get; set; }

    private double _timer = 3;

    public void Start(UserDto user)
    {
      isActive = true;
    }

    public void Update(UserDto user, double deltaTimeSec)
    {
      var newTime = _timer - deltaTimeSec;
      if((int)newTime < (int)_timer)
      {
        user.Hp -= 1;
      }

      _timer = newTime;
      if( _timer <= 0)
      {
        user.Posteffects.Remove(this);
      }
    }
  }
}
