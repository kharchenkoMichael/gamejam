using SignalRChat.Model.Dto;
using System.Collections.Generic;

namespace SignalRChat.Model
{
  public class GameContext
  {
    public List<UserDto> Users { get; set; }
    private static GameContext _instance;
    public Dictionary<int, Room> Rooms = 
            new Dictionary<int, Room> { { 1, new Room(1,"MagicForest")},{ 2, new Room(2,"NightBall")} };

    public static GameContext Instance => _instance ?? (_instance = new GameContext());

    private GameContext()
    {
      Users = new List<UserDto>();
    }
  }
}