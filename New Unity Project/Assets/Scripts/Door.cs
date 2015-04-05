using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    [SerializeField]
    bool isOpen = false;
    [SerializeField]
    float animationTime = 1f;

    float elapsed;//elapsed time since last animation

    void Update()
    {
        elapsed += Time.deltaTime;
        if (isOpen)
            transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0, -1, 0, 1), elapsed / animationTime);
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0, 0, 0, 1), elapsed / animationTime);
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && elapsed < animationTime)
        {
            print(Time.time + ":\t" + other.name + " pressed E!");
            switchPosition();
        }
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
