using System.Collections.Generic;
using Assets.Scripts.Model.Entities;
using Model.Dto;

namespace Model
{
  public class GameContext
  {
    public List<UserDto> Users { get; set; }
    private static GameContext _instance;
    public Dictionary<int, Room> Rooms = new Dictionary<int, Room> { { 1, new Room(1,"MagicForest")},{ 2, new Room(2,"NightBall")} };

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