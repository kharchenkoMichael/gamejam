using SignalRChat.Model.Dto;
using SignalRChat.Model.MagicFolders;
using System.Collections.Generic;

namespace SignalRChat.Model
{
  public class GameContext
  {
    public List<UserDto> Users;
    private static GameContext _instance;
    public Dictionary<int, Room> Rooms = new Dictionary<int, Room>
    {
      { 1, new Room(1,"MagicForest")},
      { 2, new Room(2,"NightBall")}
    };

    private List<Magic> _unresolvedSpellsCasts = new List<Magic>();

    public static GameContext Instance => _instance ?? (_instance = new GameContext());

    private GameContext()
    {
      Users = new List<UserDto>();
    }

    public void AddSpell(Magic spell)
    {
      _unresolvedSpellsCasts.Add(spell);
    }

    public List<Magic> GetAllSpells()
    {
      return _unresolvedSpellsCasts;
    }
  }
}