using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

public class FadingScript : MonoBehaviour
{
    [System.NonSerialized]
    public Text txt;
    public int status = 0;
    float timeSinceLastFadeout;

    [SerializeField]
    public float fadeInTime = 1.0f;
    public float fadeOutTime = 2.0f;

    // Use this for initialization
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        txt.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (status == 0)
            txt.text = "Press Z,Q,S,D or arrow keys to move";
        if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && status == 0)
            fadeOut();
        if (Time.time - timeSinceLastFadeout > fadeOutTime && status == 1)
            fadeIn("Press spacebar to jump");
        if (Input.GetButton("Jump") && status == 1)
            fadeOut();
        if (Input.GetButton("Run") && status == 2)
            fadeOut();
        if (Input.GetButton("Crawl") && status == 4)
            fadeOut();
        if (Time.time - timeSinceLastFadeout > fadeOutTime && status == 5)
        {
            txt.GetComponent<RectTransform>().sizeDelta = new Vector2(450f, 300f);
            fadeIn("You can't get up if you are crawling under some obstacle");
        }
        if (Time.time - timeSinceLastFadeout > fadeOutTime && status == 6)
            fadeIn("Climb up the ladder with spacebar");
        if (Time.time - timeSinceLastFadeout > fadeOutTime && status == 7)
        {
            txt.GetComponent<RectTransform>().sizeDelta = new Vector2(1000f, 300f);
            fadeIn("Congratulations! You reached the end of the 2 first levels. Please visit suicide-squad.esy.es to get more information about the game development");
        }

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
