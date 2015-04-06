using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    Vector3 offset = Vector3.up * 10;
    [SerializeField]
    float animationTime = 5f;
    [SerializeField]
    float idleTime1 = 5f;
    [SerializeField]
    float idleTime2 = 5f;
    [SerializeField]
    float wait = 0f;

    public new bool enabled = true;

    Vector3 origin;
    float totalAnimation1, totalAnimation2;
    float time;

    void Start()
    {
        origin = transform.position;
        totalAnimation1 = animationTime + idleTime1;
        totalAnimation2 = animationTime + idleTime2;
        time = -wait;
    }

    void FixedUpdate()
    {
        if (enabled)
        {
            time += Time.deltaTime;
            if (time < 0)
                origin = transform.position;
            time %= totalAnimation1 + totalAnimation2;
            if (time < animationTime)
                transform.position = Vector3.Lerp(origin, origin + offset, time / animationTime);
            if (totalAnimation1 < time && time < totalAnimation1 + animationTime)
                transform.position = Vector3.Lerp(origin + offset, origin, (time - totalAnimation1) / animationTime);
        }
    }
}
