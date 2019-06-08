﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Model.MagicFolder;
using Model.Dto;

namespace Assets.Scripts.Model.Spells
{
  public class FireBallPosteffect : ISpellPosteffect
  {
    public MagicType Type { get { return MagicType.Fireball; } }

    private double _timer = 3;

    public void Start(UserDto user)
    {      
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