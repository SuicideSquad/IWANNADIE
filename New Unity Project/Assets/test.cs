using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject.Find("SimpleDoor/Charniere").GetComponent<Door>().switchPosition();
            GameObject.Find("SimpleDoor 1/Charniere").GetComponent<Door>().switchPosition();
        }
    }
}
