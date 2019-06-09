using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Model.MagicFolder;
using Model.Dto;

namespace Assets.Scripts.Model.Spells
{
  public class InvisiblePostEffect : ISpellPosteffect
  {
    public MagicType Type { get { return MagicType.Fireball; } }
    public bool isActive { get; set; }

    public void Start(UserDto user)
    {
      isActive = true;
    }

    public void Update(UserDto user, double deltaTimeSec)
    {
    }
  }
}
