using SignalRChat.Model.Dto;
using SignalRChat.Model.MagicFolders;

namespace SignalRChat.Model.Spells
{
  public interface ISpellPosteffect
  {
    MagicType Type { get; }
    bool isActive { get; }

    void Start(UserDto user);
    void Update(UserDto user, double deltaTimeSec);
  }
}
