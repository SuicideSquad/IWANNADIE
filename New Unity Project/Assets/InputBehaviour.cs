using UnityEngine;
using System.Collections;

public class InputBehaviour : MonoBehaviour {

    CharacterMotor cm;

	void Start () {
        cm = gameObject.GetComponent<CharacterMotor>();
	}

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            Time.timeScale = 0.5f;
        else
            Time.timeScale = 1f;

        if (Input.GetButton("Run") && cm.grounded)
            cm.isRunning = true;
        if (!Input.GetButton("Run") && cm.grounded)
            cm.isRunning = false;

        if (Input.GetButton("Crouch") && cm.grounded)
            cm.isCrouching = true;
        if (!Input.GetButton("Crouch") && cm.grounded)
            cm.isCrouching = false;
        cm.jumping.enabled = !cm.isCrouching;
	}
}
