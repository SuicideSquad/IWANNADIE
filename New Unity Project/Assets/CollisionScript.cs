using UnityEngine;
using System.Collections;

public class CollisionScript : MonoBehaviour {

	// Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        FadingScript fs = GameObject.Find("First Person Controller/Canvas/Text").GetComponent<FadingScript>();
        fs.fadeIn("Press shift to run");
    }
}
