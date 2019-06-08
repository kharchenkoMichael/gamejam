using Assets.Scripts.Model.MagicFolder;
using Model.Dto;
using System.Collections.Generic;

namespace Assets.Scripts.Model.Spells
{
  public interface ISpellPosteffect
  {
    MagicType Type { get; }

    void Start(UserDto user);
    void Update(UserDto user, double deltaTimeSec);
  }
}
