﻿using System;
using System.Linq;
using System.Threading;
using Microsoft.AspNet.SignalR;
using SignalRChat.Model.Dto;

namespace SignalRChat.Model
{
  public class GameBroadcast
  {
    private readonly static Lazy<GameBroadcast> _instance = new Lazy<GameBroadcast>(() => new GameBroadcast());
    private readonly TimeSpan Interval = TimeSpan.FromMilliseconds(40);
    private readonly IHubContext _hubContext;
    private Timer _loop;

    private bool _userUpdeted;
    private bool _roomIdsUpdated;
    private bool _spellsCastUpdate;
    private bool _startGame;

    public GameBroadcast()
    {
      _hubContext = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
      _userUpdeted = false;
      _loop = new Timer(Broadcast, null, Interval, Interval);
    }

    public void Broadcast(object state)
    {
      if (_userUpdeted)
      {
        _hubContext.Clients.All.refreshUsers(GameContext.Instance.Users);
        _userUpdeted = false;
      }

      if (_roomIdsUpdated)
      {
        var allRooms = new RoomUpdateDto() { Rooms = GameContext.Instance.Rooms.Select(r => r.Value).ToList() };
        _hubContext.Clients.All.refreshRoomIds(allRooms);
        _roomIdsUpdated = false;
      }

      if(_spellsCastUpdate)
      {
        var allSpells = GameContext.Instance.GetAllSpells();
        _hubContext.Clients.All.refreshSpells(allSpells);
      }

      if (_startGame)
      {
        _hubContext.Clients.All.startGameFrom(GameContext.Instance.Users);
        _startGame = false;
      }
    }

    public void UpdateUser()
    {
      _userUpdeted = true;
    }
    
    public void UpdateRooms()
    {
      _roomIdsUpdated = true;
    }

    public void UpdateSpells()
    {
      _spellsCastUpdate = true;
    }

    public void StartGame()
    {
      _startGame = true;
    }

    public static GameBroadcast Instance => _instance.Value;
  }
}