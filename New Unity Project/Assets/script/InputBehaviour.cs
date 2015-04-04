using UnityEngine;
using System.Collections;

public class InputBehaviour : MonoBehaviour
{
    CharacterMotor cm;
    //CharacterController cc;
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
        defaultHeight = transform.localScale.y;
    }

    void Update()
    {
        if (Input.GetButton("Quit"))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.Y))
            Time.timeScale = 0.5f;
        else
            Time.timeScale = 1f;

        /*##########RUNNING##########*/
        if (Input.GetButton("Run") && cm.grounded)
            cm.isRunning = true;
        if (!Input.GetButton("Run") && cm.grounded)
            cm.isRunning = false;

        /*##########CRAWLING##########*/
        elapsedSinceCrawling += Time.deltaTime;
        elapsedSinceUncrawling += Time.deltaTime;
        if (Input.GetButton("Crawl") && cm.grounded && !cm.isCrawling && !cm.isClimbing)
        {
            cm.isCrawling = true;
            elapsedSinceCrawling = 0;
        }
        if (!Input.GetButton("Crawl") && cm.grounded && cm.isCrawling)
        {
            cm.isCrawling = false;
            elapsedSinceUncrawling = 0;
        }
        if (Physics.Raycast(transform.position, Vector3.up, 1f) && !cm.isCrawling)
            cm.isCrawling = true;
        cm.jumping.enabled = !cm.isCrawling;
        if (cm.isCrawling)
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, defaultHeight * crawlingHeightFactor, elapsedSinceCrawling / animationTime), transform.localScale.x);
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, defaultHeight, elapsedSinceUncrawling / animationTime), transform.localScale.x);
            transform.Translate(Vector3.up * Mathf.Lerp(0, defaultHeight - transform.localScale.y, elapsedSinceUncrawling / animationTime));
        }
    }
}
