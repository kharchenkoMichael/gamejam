using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Model.MagicFolder;
using Model.Dto;

namespace Assets.Scripts.Model.Spells
{
  public class MagicAmbrellaPosteffect : ISpellPosteffect
  {
    public MagicType Type { get { return MagicType.MagicUmbrella; } }
    public bool isActive { get; set; }

    private double _timer;

    public MagicAmbrellaPosteffect(double openTime)
    {
      _timer = openTime;
      isActive = false;
    }

    public void Start(UserDto user)
    {
      
    }

    public void Update(UserDto user, double deltaTimeSec)
    {
      _timer -= deltaTimeSec;
      if (_timer <= 0)
        isActive = true;
    }
  }
}
