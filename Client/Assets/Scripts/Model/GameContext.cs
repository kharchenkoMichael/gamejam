using System.Collections.Generic;

namespace Model
{
  public class GameContext
  {
    public List<User> Users { get; set; }
    private static GameContext _instance;

    public static GameContext Instance
    {
      get
      {
        _instance= _instance ?? new GameContext();
        return _instance;
      }
    }

    private GameContext()
    {
      Users = new List<User>();
    }
  }
}