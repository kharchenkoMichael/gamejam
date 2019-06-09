using Assets.Scripts.Model.MagicFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.Spells
{
  public static class PosteffectBuilder
  {
    public static ISpellPosteffect GetPosteffect(MagicType type)
    {
      switch (type)
      {
        case MagicType.Lightning:
          {
            return new LightningPosteffect();
          }
        case MagicType.Stonefall:
          {
            return new StonefallPosteffect();
          }
        case MagicType.Fireball:
          {
            return new FireBallPosteffect();
          }
        case MagicType.IceBolt:
          {
            return new IceBoltPosteffect();
          }
        default: return null;
      }
    }
  }
}
