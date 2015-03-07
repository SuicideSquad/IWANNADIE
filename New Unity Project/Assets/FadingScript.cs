using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

public class FadingScript : MonoBehaviour
{

    Text txt;
    int status;

    [SerializeField]
    public float fadingTime = 2.0f;

    // Use this for initialization
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        txt.text = "Press Z,Q,S,D or arrow keys to move";
        status = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical") && status == 0)
        {
            txt.CrossFadeAlpha(0f, fadingTime, false);
            status++;
        }
    }
}
