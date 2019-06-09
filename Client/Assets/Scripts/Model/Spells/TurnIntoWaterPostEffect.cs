using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Model.MagicFolder;
using Model.Dto;

namespace Assets.Scripts.Model.Spells
{
  public class TurnIntoWaterPostEffect : ISpellPosteffect
  {
    public MagicType Type { get { return MagicType.TurnIntoWater; } }
    public bool isActive { get; set; }

    private double _timer = 10;
    
    public void Start(UserDto user)
    {
      isActive = true;
    }

    public void Update(UserDto user, double deltaTimeSec)
    {
      _timer -= deltaTimeSec;
      if(_timer<=0)
      {
        user.Posteffects.Remove(this);
      }
    }
  }
}
