using System.Collections.Generic;

namespace SignalRChat.Model
{
  public class GameContext
  {
    public List<User> Users { get; set; }
    private static GameContext _instance;
    public Dictionary<int, string> Rooms = new Dictionary<int, string> { { 1,"MagicForest"},{ 2, "NightBall"} };

    public static GameContext Instance => _instance ?? (_instance = new GameContext());

    private GameContext()
    {
      Users = new List<User>();
    }
  }
}