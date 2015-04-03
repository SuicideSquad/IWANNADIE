using UnityEngine;
using System.Collections;

public class InputBehaviour : MonoBehaviour
{

    CharacterMotor cm;
    CharacterController cc;
    float defaultHeight;

    [SerializeField]
    float crawlingHeightFactor = 0.2f;

    void Start()
    {
        cm = gameObject.GetComponent<CharacterMotor>();
        cc = gameObject.GetComponent<CharacterController>();
        defaultHeight = cc.height;
    }

    void Update()
    {
		if (Input.GetButton("Quit")) {
			Application.Quit();
				}
        if (Input.GetKey(KeyCode.A))
            Time.timeScale = 0.5f;
        else
            Time.timeScale = 1f;

        if (Input.GetButton("Run") && cm.grounded)
            cm.isRunning = true;
        if (!Input.GetButton("Run") && cm.grounded)
            cm.isRunning = false;

        if (Input.GetButton("Crawl") && cm.grounded && !cm.isCrawling && !cm.isClimbing)
            cm.isCrawling = true;
        if (!Input.GetButton("Crawl") && cm.grounded
            && !Physics.Raycast(gameObject.transform.position, Vector3.up, defaultHeight * (1 - crawlingHeightFactor))
            && !Physics.Raycast(gameObject.transform.position, Vector3.forward,1f))
            cm.isCrawling = false;
        cm.jumping.enabled = !cm.isCrawling;
        if (cm.isCrawling)
        {
            cc.height = Mathf.Lerp(cc.height, defaultHeight * crawlingHeightFactor, Time.deltaTime * 10);
        }
        else
        {
            cc.height = Mathf.Lerp(cc.height, defaultHeight, Time.deltaTime * 10);
        }
    }
}
