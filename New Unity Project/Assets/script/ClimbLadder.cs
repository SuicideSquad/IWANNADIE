using UnityEngine;
using System.Collections;

public class ClimbLadder : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        CharacterMotor cm = GameObject.Find("First Person Controller").GetComponent<CharacterMotor>();
        cm.grounded = true;
    }
}
