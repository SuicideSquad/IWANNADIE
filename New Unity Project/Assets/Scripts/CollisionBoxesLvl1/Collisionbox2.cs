using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Collisionbox2 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        GameObject txt = GameObject.Find("First Person Controller/Canvas/Text");
        txt.GetComponent<RectTransform>().sizeDelta = new Vector2(400f, 300f);
        FadingScript fs = txt.GetComponent<FadingScript>();
        fs.status = 3;
        fs.fadeIn("Jumping while running can make you jump further");
    }
}
