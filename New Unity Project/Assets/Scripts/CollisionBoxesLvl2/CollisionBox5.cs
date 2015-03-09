using UnityEngine;
using System.Collections;

public class CollisionBox5 : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        FadingScript fs = GameObject.Find("First Person Controller/Canvas/Text").GetComponent<FadingScript>();
        fs.fadeOut();
    }
}
