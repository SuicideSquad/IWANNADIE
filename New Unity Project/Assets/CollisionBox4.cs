using UnityEngine;
using System.Collections;

public class CollisionBox4 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        FadingScript fs = GameObject.Find("First Person Controller/Canvas/Text").GetComponent<FadingScript>();
        fs.status = 4;
        fs.fadeIn("Press X to crawl");
    }
}
