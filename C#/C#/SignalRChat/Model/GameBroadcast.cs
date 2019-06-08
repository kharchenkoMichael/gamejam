using System;
using System.Threading;
using Microsoft.AspNet.SignalR;

namespace SignalRChat.Model
{
  public class GameBroadcast
  {
    private readonly static Lazy<GameBroadcast> _instance = new Lazy<GameBroadcast>(() => new GameBroadcast());
    private readonly TimeSpan Interval = TimeSpan.FromMilliseconds(40);
    private readonly IHubContext _hubContext;
    private Timer _loop;

    private bool _modelUpdated;

    public GameBroadcast()
    {
      _hubContext = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
      _modelUpdated = false;
      _loop = new Timer(Broadcast, null,Interval,Interval);
    }

    public void Broadcast(object state)
    {
      if (!_modelUpdated) 
        return;
      
      _hubContext.Clients.All.refresh(GameContext.Instance.Users);
      _modelUpdated = false;
    }

    public void Update()
    {
      _modelUpdated = true;
    }

    public static GameBroadcast Instance => _instance.Value;
  }
}