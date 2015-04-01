using UnityEngine;
using System.Collections;

public class ClimbLadder : MonoBehaviour
{
    [SerializeField]
    float climbingSpeed = 0.05f;
    void OnTriggerStay(Collider other)
    {
        CharacterMotor cm = GameObject.Find("First Person Controller").GetComponent<CharacterMotor>();
        if (Input.GetButton("Jump"))
        {
            cm.grounded = true;
            cm.transform.Translate(Vector3.up * climbingSpeed);
        }
    }
}
