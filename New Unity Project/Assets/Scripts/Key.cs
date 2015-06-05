using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Key : MonoBehaviour {
    public Image Key_1; 
	// Use this for initialization
	void Start () 
    {
        Key_1.enabled = false;
	}

    void PickaKey()
    {
        Key_1.enabled = true;
    }
    void UseaKey()
    {
        Key_1.enabled = false;
    } 
	

}
