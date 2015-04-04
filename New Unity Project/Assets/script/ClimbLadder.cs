using UnityEngine;
using System.Collections;

public class ClimbLadder : MonoBehaviour
{
    [SerializeField]
    float climbingSpeed = 5f;

    CharacterMotor cm;
    float grav;

    void Start()
    {
        cm = GameObject.Find("First Person Controller").GetComponent<CharacterMotor>();
        grav = cm.movement.gravity;
    }

    void OnTriggerEnter(Collider other)
    {
        cm.movement.gravity = 0;
        cm.movement.velocity = Vector3.zero;
        cm.isClimbing = true;
    }

    void OnTriggerStay(Collider other)
    {
        cm.movement.velocity.y = 0f;
        if (Input.GetButton("Jump"))
            cm.transform.Translate(Vector3.up * climbingSpeed * Time.deltaTime);
        if (Input.GetButton("Crawl") && !Physics.Raycast(cm.transform.position, Vector3.down, 1.05f))
            cm.transform.Translate(Vector3.down * climbingSpeed * Time.deltaTime);
    }

    void OnTriggerExit(Collider other)
    {
        cm.movement.gravity = grav;
        cm.isClimbing = false;
    }
}
