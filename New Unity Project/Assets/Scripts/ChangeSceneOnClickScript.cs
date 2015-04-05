using UnityEngine;
using System.Collections;

public class ChangeSceneOnClickScript : MonoBehaviour {

	public string _nextScene = "";

	public void OnTriggerStay()
	{
		if (Input.GetKeyDown("e") || (Input.touches != null && Input.touches.Length > 0))
		{
			Application.LoadLevel(_nextScene);
		}
	}
}
