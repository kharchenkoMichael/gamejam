using UnityEngine;
using UnityEngine.UI;

public class AlertScript : MonoBehaviour
{
  public NewBehaviourScript SignalR;

  public Text Text;

  public void SetText(string text)
  {
    Text.text = text;
  }

  public void Close()
  {
    SignalR.ClosePopup();
  }

}
