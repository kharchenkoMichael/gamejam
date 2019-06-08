using System.Collections.Generic;

namespace SignalRChat.Model
{
  public class GameContext
  {
    public List<User> Users { get; set; }
    private static GameContext _instance;

    public static GameContext Instance => _instance ?? (_instance = new GameContext());

    private GameContext()
    {
      Users = new List<User>();
    }
  }
}