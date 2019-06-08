using System.Collections.Generic;
using Model.Dto;

namespace Model
{
  public class GameContext
  {
    public List<UserDto> Users { get; set; }
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
      Users = new List<UserDto>();
    }
  }
}