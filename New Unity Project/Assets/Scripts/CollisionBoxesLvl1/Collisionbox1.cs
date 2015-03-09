using UnityEngine;
using System.Collections;

public class Collisionbox1 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        FadingScript fs = GameObject.Find("First Person Controller/Canvas/Text").GetComponent<FadingScript>();
        fs.status = 2;
        fs.fadeIn("Press shift to run");
    }
}
