using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    [SerializeField]
    bool isOpen = false;
    [SerializeField]
    float animationTime = 1f;

    float elapsed;

    void Update()
    {
        elapsed += Time.deltaTime;
        if (isOpen)
            transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0, -1, 0, 1), elapsed/animationTime);
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0, 0, 0, 1), elapsed/animationTime);
        if (elapsed > 2)
        {
            elapsed = 0;
            print("hey " + Time.time);
            isOpen = !isOpen;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        print("truc");
        if (Input.GetKey(KeyCode.E))
        {
            isOpen = !isOpen;
            elapsed = 0;
        }
    }
}
