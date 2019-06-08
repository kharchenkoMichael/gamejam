using System.Linq;
using Model;
using UnityEngine;

public class ChoosedAbility : MonoBehaviour
{
  public NewBehaviourScript SignalR;

  public int[] CreatorMagic = new int[2];
  public int[] OpponentMagic = new int[2];

  public GameObject[] MyAbility;
  public GameObject[] OpponentAbility;

  private void Update()
  {
    if (SignalR.RoomId == 0)
    {
      SetAbbilities();
      return;
    }

    var room = GameContext.Instance.Rooms[SignalR.RoomId];
    if (!room.Users.Any())
    {
      SetAbbilities();
      return;
    }
    
    var creatorMagics = GameContext.Instance.Users.Find(item => item.Name == room.Users[0]).Magic;
    if (creatorMagics.Count > 0)
      CreatorMagic[0] = creatorMagics[0];
    if (creatorMagics.Count > 1)
      CreatorMagic[1] = creatorMagics[1];
    
    if (room.Users.Count > 1)
    {
      var opponentMagic = GameContext.Instance.Users.Find(item => item.Name == room.Users[1]).Magic;
      if (opponentMagic.Count > 0)
        OpponentMagic[0] = opponentMagic[0];
      if (opponentMagic.Count > 1)
        OpponentMagic[1] = opponentMagic[1];
    }
    SetAbbilities();
  }

  private void SetAbbilities()
  {
    MyAbility[0].SetActive(CreatorMagic[0]!=0);
    MyAbility[1].SetActive(CreatorMagic[1]!=0);
    OpponentAbility[0].SetActive(OpponentMagic[0]!=0);
    OpponentAbility[1].SetActive(OpponentMagic[1]!=0);
  }
}
