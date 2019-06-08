using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoveSliderScript : MonoBehaviour
{
	public Slider Slide;

	IEnumerator Start()
	{
		for (int i = 0; i < 10; i++)
		{
			yield return new WaitForSeconds(0.1f);
			Slide.value = (i+1) * 100;
		}
	}
}
