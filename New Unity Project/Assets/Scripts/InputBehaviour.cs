using UnityEngine;
using System.Collections;

public class InputBehaviour : MonoBehaviour
{
    CharacterMotor cm;
    Attributes attr;
    float defaultHeight;
    float elapsedSinceUncrawling;
    float elapsedSinceCrawling;

    [SerializeField]
    float crawlingHeightFactor = 0.5f;
    [SerializeField]
    float animationTime = 1f;

    void Start()
    {
        cm = GetComponent<CharacterMotor>();
        attr = GetComponent<Attributes>();
        defaultHeight = transform.localScale.y;

        //so that they don't screw up the start of the game
        elapsedSinceCrawling = float.MaxValue;
        elapsedSinceUncrawling = float.MaxValue;
    }

    void Update()
    {
        if (Input.GetButton("Quit"))
        {
            Application.LoadLevel("MENU");
        }
        if (Input.GetKey(KeyCode.T))
            Time.timeScale = 0.5f;
        else
            Time.timeScale = 1f;

		/*##########RUNNING##########*/
		attr.isRunning = attr.canRun;
        if (Input.GetButton("Run") && attr.canRun && cm.grounded)
            attr.isRunning = true;
        if (!Input.GetButton("Run") && cm.grounded)
            attr.isRunning = false;

        /*##########CRAWLING##########*/
        elapsedSinceCrawling += Time.deltaTime;
        elapsedSinceUncrawling += Time.deltaTime;
        if (Input.GetButton("Crawl") && cm.grounded && !attr.isCrawling && !attr.isClimbing)
        {
            attr.isCrawling = true;
            elapsedSinceCrawling = 0;
        }
        if (!Input.GetButton("Crawl") && cm.grounded && attr.isCrawling)
        {
            attr.isCrawling = false;
            elapsedSinceUncrawling = 0;
        }
        if (Physics.Raycast(transform.position, Vector3.up, 1f) && !attr.isCrawling)
            attr.isCrawling = true;
        cm.jumping.enabled = !attr.isCrawling;
        if (attr.isCrawling)
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, defaultHeight * crawlingHeightFactor, elapsedSinceCrawling / animationTime), transform.localScale.x);
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, defaultHeight, elapsedSinceUncrawling / animationTime), transform.localScale.x);
            if (elapsedSinceUncrawling / animationTime <= 1)
                transform.Translate(Vector3.up * Mathf.Lerp(0, defaultHeight - transform.localScale.y, (elapsedSinceUncrawling + Time.deltaTime) / animationTime));
        }

        /*##########APPLYING CHANGES ON SPEED##########*/
        cm.movement.maxForwardSpeed = cm.movement.maxForwardWalkingSpeed;
        cm.movement.maxBackwardSpeed = cm.movement.maxBackwardWalkingSpeed;
        if (attr.isRunning)
            cm.movement.maxForwardSpeed = cm.movement.maxForwardRunSpeed;
        if (attr.isCrawling)
        {
            cm.movement.maxForwardSpeed = cm.movement.maxCrawlSpeed;
            cm.movement.maxBackwardSpeed = cm.movement.maxCrawlSpeed;
        }
    }
}
