using UnityEngine;
using System.Collections;

public class die : MonoBehaviour {
void OnTriggerEnter(Collider C){
		Application.LoadLevel (Application.loadedLevel+1);
}}
