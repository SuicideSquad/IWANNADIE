using UnityEngine;

using System.Collections;
using UnityEngine.UI;

public class FadingScript : MonoBehaviour
{

    Text txt;
    [System.NonSerialized]
    public int status;
    float timeSinceLastFadeout;

    [SerializeField]
    public float fadeInTime = 1.0f;
    public float fadeOutTime = 2.0f;

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
        if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && status == 0)
            fadeOut();
        if (Time.time - timeSinceLastFadeout > fadeOutTime && status == 1)
            fadeIn("Press spacebar to jump");
        if (Input.GetButton("Jump") && status == 1)
            fadeOut();
        if (Input.GetButton("Run") && status == 2)
            fadeOut();
    }

    public void fadeIn(string text)
    {
        txt.text = text;
        txt.CrossFadeAlpha(1f, fadeInTime, false);
        timeSinceLastFadeout = float.MaxValue;
    }

    public void fadeOut()
    {
        txt.CrossFadeAlpha(0f, fadeOutTime, false);
        timeSinceLastFadeout = Time.time;
        status++;
    }
}
