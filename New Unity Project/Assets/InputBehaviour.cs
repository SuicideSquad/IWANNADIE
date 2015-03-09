using UnityEngine;
using System.Collections;

public class InputBehaviour : MonoBehaviour
{

    CharacterMotor cm;
    CharacterController cc;
    float defaultHeight;

    [SerializeField]
    float crouchingHeightFactor = 0.2f;

    void Start()
    {
        cm = gameObject.GetComponent<CharacterMotor>();
        cc = gameObject.GetComponent<CharacterController>();
        defaultHeight = cc.height;
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

        if (Input.GetButton("Crouch") && cm.grounded && !cm.isCrouching)
            cm.isCrouching = true;
        if (!Input.GetButton("Crouch") && cm.grounded && !Physics.Raycast(gameObject.transform.position, Vector3.up, defaultHeight * (1 - crouchingHeightFactor)))
            cm.isCrouching = false;
        cm.jumping.enabled = !cm.isCrouching;
        if (cm.isCrouching)
        {
            cc.height = Mathf.Lerp(cc.height, defaultHeight * crouchingHeightFactor, Time.deltaTime * 10);
        }
        else
        {
            cc.height = Mathf.Lerp(cc.height, defaultHeight, Time.deltaTime * 10);
        }
    }
}
