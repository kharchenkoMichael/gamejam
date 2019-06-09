using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Model.Dto;
using Microsoft.AspNet.SignalR.Client;
using Model;
using Model.Dto;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.BehaviorScripts;
using Assets.Scripts.Model.MagicFolder;

public enum Form
{
  LoadingForm,
  StartForm,
  RoomForm,
  Game
}

public class NewBehaviourScript : MonoBehaviour
{
  public GameObject Capsule;

  public Dictionary<int, GameObject> Forms = new Dictionary<int, GameObject>();

  public GameObject Alert;
  public GameObject LoadingForm;
  public GameObject StartForm;
  public GameObject RoomForm;
  public GameObject GameForm;
  public GameObject MagicContainer;
  public GameObject[] Rooms;

  public GameObject StartButton;
  public GameObject CastMagicSpellA;
  public GameObject CastMagicSpellB;

  private List<GameObject> _users = new List<GameObject>();
  public string _name = string.Empty;

  private bool _startGame = false;
  private bool _refreshUser = false;
  private bool _refreshRoom = false;
  private bool _createRoom = false;
  private bool _refreshSpells = false;
  private bool _oponentQuit = false;

  private GameObject _userCreator;
  private GameObject _opponent;

  public int RoomId;

  public string signalUrl;

  private HubConnection _hubConnection = null;

  private IHubProxy _hubProxy;

  private bool _magicUpdated;
  private MagicManager _magicManager;
  private void InitializeDictionary()
  {
    Forms[(int)Form.LoadingForm] = LoadingForm;
    Forms[(int)Form.StartForm] = StartForm;
    Forms[(int)Form.RoomForm] = RoomForm;
    Forms[(int)Form.Game] = GameForm;
  }
  // Use this for initialization
  IEnumerator Start()
  {
    InitializeDictionary();
    OpenForm(Form.LoadingForm);
    Debug.Log("Start()");
    yield return new WaitForSeconds(1);

    Debug.Log("Start() 1 second.");
    StartSignalR();
    OpenForm(Form.StartForm);
    InitializeMagic();
  }
  private void InitializeMagic()
  {
    var magic = MagicContainer.GetComponent<MagicContainerScript>();
    _magicManager = new MagicManager();
    magic.InitializeMagic(_magicManager);
    var elements = magic.Elements;
    foreach (var element in elements)
    {
      element.Value.GetComponent<MagicScript>().ActionDelegate += ChooseMagic;
    }

    StartButton.GetComponent<StartGameScript>().ActionDelegate += StartGame;
  }

  private void StartSignalR()
  {
    Debug.Log("StartSignalR");
    if (_hubConnection == null)
    {
      _hubConnection = new HubConnection(signalUrl);
      Debug.Log(signalUrl);
      _hubConnection.Error += HubConnection_Error;

      _hubProxy = _hubConnection.CreateHubProxy("MyHub");
      _hubProxy.On<List<UserDto>>("refreshUsers", RefreshUsers);
      _hubProxy.On<RoomUpdateDto>("refreshRoomIds", RefreshRoom);
      _hubProxy.On<List<UserDto>>("startGameFrom", StartGameFrom);
      _hubProxy.On<SpellDto>("refreshSpells", RefreshSpells);

      _hubProxy.On("quit", OponentQuit);

      _hubConnection.Start().Wait();
      _hubConnection.StateChanged += change =>
      {
        Debug.Log($"{change.NewState} {change.OldState}");
      };
      Debug.Log("_hubConnection.Start();");
      GetRoomIds();
    }
    else
    {
      Debug.Log("Signalr  already connected...");
    }
  }

  private void OponentQuit()
  {
    _oponentQuit = true;
  }

  private void RefreshUsers(List<UserDto> users)
  {
    GameContext.Instance.Users = users;

    RoomId = users.Find(item => item.Name == _name).RoomId;

    _refreshUser = true;
  }

  private void RefreshRoom(RoomUpdateDto dto)
  {
    GameContext.Instance.Rooms = dto.Rooms.ToDictionary(item => item.Id, item => item);

    _refreshRoom = true;
  }

  private void RefreshSpells(SpellDto dto)
  {
    GameContext.Instance.Spell = dto;
    _refreshSpells = true;
  }

  private void StartGameFrom(List<UserDto> users)
  {
    GameContext.Instance.Users = users;
    RoomId = users.Find(item => item.Name == _name).RoomId;
    _startGame = true;
    _refreshUser = true;
  }

  private void CastFirst()
  {
    var currentUser = GameContext.Instance.Users.Find(x => x.Name == _name);
    if (!currentUser.Magic.Any())
      return;
    var magicType = _magicManager.GetAllMagic()[currentUser.Magic[0]].Type;
    var spell = new SpellDto();
    spell.SpellType = magicType;
    spell.OwnerName = _name;
    CastMagic(spell);
  }

  private void CastSecond()
  {
    var currentUser = GameContext.Instance.Users.Find(x => x.Name == _name);
    if (!currentUser.Magic.Any())
      return;
    var magicType = _magicManager.GetAllMagic()[currentUser.Magic[1]].Type;
    var spell = new SpellDto();
    spell.SpellType = magicType;
    spell.OwnerName = _name;
    CastMagic(spell);
  }

  #region Callbacks


  public void ClosePopup()
  {
    Alert.SetActive(false);
  }

  public void OpenPopup(string text)
  {
    Alert.GetComponent<AlertScript>().SetText(text);
    Alert.SetActive(true);
  }

  public void OpenForm(Form form)
  {
    foreach (var formsValue in Forms.Values)
    {
      formsValue.SetActive(false);
    }
    Forms[(int)form].SetActive(true);
  }

  public void GetRoomIds()
  {
    _hubProxy.Invoke("getRoomIds");

    Debug.Log("GetRoomIds;\n");
  }

  public void Exit()
  {
    _hubProxy.Invoke("userExit", _name);

    Debug.Log("userExit;\n");
  }

  public void Quit()
  {
    Application.Quit();
    Debug.Log("Quit;\n");
  }

  public void CreateRoom(int avatarId, string myName)
  {
    StartButton.SetActive(true);
    _name = myName;
    _hubProxy.Invoke("createRoom", myName, avatarId);
    OpenForm(Form.RoomForm);
    Debug.Log("CreateRoom;\n");
  }

  public void JoinToRoom(int roomId, string myName, int avatarId)
  {
    StartButton.SetActive(false);
    _name = myName;
    _hubProxy.Invoke("joinToRoom", myName, avatarId, roomId);
    OpenForm(Form.RoomForm);
    Debug.Log("ConnectToRoom;\n");
  }

  public void UpdateCapsul(string capsulaName)
  {
    var user = GameContext.Instance.Users.Find(item => item.Name == capsulaName);
    _hubProxy.Invoke("update", user);
  }
  
  private void ChooseMagic(int Id)
  {
    var user = GameContext.Instance.Users.Find(item => item.Name == _name);
    if (user.Magic.Count == 2)
    {
      OpenPopup("Можно выбрать только 2 магии");
      return;
    }
    if (GameContext.Instance.Rooms[user.RoomId].Users[0] == user.Name
        && (!user.Magic.Any()
            || GameContext.Instance.Users.Find(item => item.Name == GameContext.Instance.Rooms[user.RoomId].Users[1])
              .Magic.Count == 2))
    {
      user.Magic.Add(Id);
      _hubProxy.Invoke("update", user);
      Debug.Log("ChooseMagic Creator;\n");
    }
    else if (GameContext.Instance.Rooms[user.RoomId].Users[0] != user.Name
             && (GameContext.Instance.Users.Find(item => item.Name == GameContext.Instance.Rooms[user.RoomId].Users[0])
               .Magic.Any()))
    {
      user.Magic.Add(Id);
      _hubProxy.Invoke("update", user);
      Debug.Log("ChooseMagic Not creator;\n");
    }
    else
      OpenPopup("Сейчас выбирает противник");
  }

  private void StartGame()
  {
    var currentRoomUsers = GameContext.Instance.Users.Where(x => x.RoomId == RoomId);
    foreach (var user in currentRoomUsers)
    {
      if (user.Magic.Count < 2)
        return;
    }

    _hubProxy.Invoke("startGame", currentRoomUsers);
    Debug.Log("Start Game;\n");
  }

  private void CastMagic(SpellDto spell)
  {
    _hubProxy.Invoke("castMagic", spell);
    Debug.Log("Cast Spell;\n");
  }

  #endregion
  void OnApplicationPause(bool pauseStatus)
  {
    if (!pauseStatus)
      return;
    
    //Exit();
    Debug.Log($"OnApplicationPause() {Time.time} seconds");
    _hubConnection.Error -= HubConnection_Error;
    _hubConnection.Stop();
  }

  void OnAppliacationQuit()
  {
    Exit();
    Debug.Log($"OnAppliacationQuit() {Time.time} seconds");
    _hubConnection.Error -= HubConnection_Error;
    _hubConnection.Stop();
  }

  private void HubConnection_Error(Exception obj)
  {
    Debug.Log("Hub Error - " + obj.Message + Environment.NewLine +
              obj.Data + Environment.NewLine +
              obj.StackTrace + Environment.NewLine +
              obj.TargetSite);
  }

  private void hubConnection_Closed()
  {
    Debug.Log("Closed");
  }


  // Update is called once per frame
  void Update()
  {
    if (_startGame)
      StartGameUpdate();
    if (_refreshUser)
      RefreshUserUpdate();
    if (_refreshRoom)
      RefreshRoomUpdate();
    if (_createRoom)
    {
      StartButton.SetActive(true);
      OpenForm(Form.RoomForm);
      _createRoom = false;
    }
    if (_refreshSpells)
    {
      RefreshSpellUpdate();
    }
    if (_oponentQuit)
    {
      Destroy(_userCreator);
      Destroy(_opponent);
      _userCreator = null;
      _opponent = null;
      OpenForm(Form.StartForm);
      OpenPopup("ваш противник вышел из игры");
      _oponentQuit = false;
    }
  }

  private void RefreshSpellUpdate()
  {
    var spell = GameContext.Instance.Spell;
    if (spell == null)
      return;

    var spellOwner = _userCreator.GetComponent<CapsulScript>().Name == spell.OwnerName ? _userCreator : _opponent;
    spellOwner.GetComponent<GameBehaviorScript>().Spell(spell);
    _refreshSpells = false;
  }
  

  private void RefreshUserUpdate()
  {
    foreach (var user in GameContext.Instance.Users)
    {
      var elements = MagicContainer.GetComponent<MagicContainerScript>().Elements;
      foreach (var magicId in user.Magic)
      {
        elements[magicId].SetActive(false);
      }
    }

    if (_userCreator == null || _opponent == null)
    {
      _refreshUser = false;
      _magicUpdated = false;
      return;
    }

    var creatorCapsul = _userCreator.GetComponent<CapsulScript>();
    var oponentCapsul = _opponent.GetComponent<CapsulScript>();

    var creator = GameContext.Instance.Users.Find(item => item.Name == creatorCapsul.Name);
    var oponent = GameContext.Instance.Users.Find(item => item.Name == oponentCapsul.Name);

    creatorCapsul.SetByUser(creator);
    oponentCapsul.SetByUser(oponent);

    _userCreator.transform.LookAt(_opponent.transform);
    _opponent.transform.LookAt(_userCreator.transform);

    _refreshUser = false;
    _magicUpdated = false;
  }

  private void RefreshRoomUpdate()
  {
    var i = 0;
    foreach (var room in GameContext.Instance.Rooms)
    {
      var userCreator = room.Value.Users.FirstOrDefault();
      if (userCreator == null)
      {
        Rooms[i].SetActive(false);
        i++;
        continue;
      }

      Rooms[i].GetComponent<RoomScript>().SetRoom(room.Key, this, StartForm.GetComponent<StartFormScript>(), room.Value.Name, userCreator);
      i++;
    }

    _refreshRoom = false;
  }

  private void StartGameUpdate()
  {
    OpenForm(Form.Game);

    var creator = GameContext.Instance.Users.Find(item => item.Name == GameContext.Instance.Rooms[RoomId].Users[0]);
    var opponent = GameContext.Instance.Users.Find(item => item.Name == GameContext.Instance.Rooms[RoomId].Users[1]);

    _userCreator = Instantiate(Capsule,
      new Vector3(creator.Position.PositionX, creator.Position.PositionY, creator.Position.PositionZ),
      Quaternion.identity);

    _opponent = Instantiate(Capsule,
      new Vector3(opponent.Position.PositionX, opponent.Position.PositionY, opponent.Position.PositionZ),
      Quaternion.identity);

    var creatorBehavior = _userCreator.AddComponent<GameBehaviorScript>();
    creatorBehavior.Enemy = _opponent;
    creatorBehavior.User = GameContext.Instance.Users.Find(item => item.Name == GameContext.Instance.Rooms[RoomId].Users[0]);
    creatorBehavior.SignalR = this;
    
    var opponentBehavior = _opponent.AddComponent<GameBehaviorScript>();
    opponentBehavior.Enemy = _userCreator;
    opponentBehavior.User = GameContext.Instance.Users.Find(item => item.Name == GameContext.Instance.Rooms[RoomId].Users[1]);
    opponentBehavior.SignalR = this;

    _userCreator.transform.LookAt(_opponent.transform);
    _opponent.transform.LookAt(_userCreator.transform);

    _userCreator.GetComponent<CapsulScript>().SetName(creator.Name, this);
    _opponent.GetComponent<CapsulScript>().SetName(opponent.Name, this);

    _userCreator.name = creator.Name;
    _opponent.name = opponent.Name;

    CastMagicSpellA.GetComponent<MagicCastScript>().ActionDelegate += CastFirst;
    CastMagicSpellB.GetComponent<MagicCastScript>().ActionDelegate += CastSecond;

    _startGame = false;
  }
}
