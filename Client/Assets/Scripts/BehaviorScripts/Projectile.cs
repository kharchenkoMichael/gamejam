using UnityEngine;

namespace Assets.Scripts.BehaviorScripts
{
  public class Projectile : MonoBehaviour
  {
    private void OnTriggerEnter(Collider other)
    {
      Destroy(gameObject);
    }
  }
}