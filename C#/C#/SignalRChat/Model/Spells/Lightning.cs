using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalRChat.Model.Dto;
using SignalRChat.Model.MagicFolders;

namespace Assets.Scripts.Model.Spells
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
