using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    [SerializeField]
    bool isOpen = false;
    [SerializeField]
    float animationTime = 1f;
    [SerializeField]
    bool reversed = false;

    float elapsed;//elapsed time since last animation

    void Update()
    {
        elapsed += Time.deltaTime;
        if (isOpen)
            transform.localRotation = Quaternion.Slerp(transform.localRotation, new Quaternion(0, reversed ? 1 : -1, 0, 1), elapsed / animationTime);
        else
            transform.localRotation = Quaternion.Slerp(transform.localRotation, new Quaternion(0, 0, 0, 1), elapsed / animationTime);
    }

    public void switchPosition()
    {
        elapsed = 0;
        isOpen = !isOpen;
    }

    public void open()
    {
        print("opening door");
        elapsed = 0;
        isOpen = true;
    }

    public void close()
    {
        print("closing door");
        elapsed = 0;
        isOpen = false;
    }
}
