using UnityEngine;
using System.Collections;

public class ClimbLadder : MonoBehaviour
{
    [SerializeField]
    float verticalSpeed = 1.0f;

    void OnTriggerStay(Collider other)
    {
        CharacterMotor cm = GameObject.Find("First Person Controller").GetComponent<CharacterMotor>();
        cm.grounded = true;
        
    }
}
