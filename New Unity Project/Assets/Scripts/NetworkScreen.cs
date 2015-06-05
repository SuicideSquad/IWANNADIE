using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;

public class NetworkScreen : MonoBehaviour
{
    bool opened = false;
    float start = -1;
    Vector3[] phases = new Vector3[]{
        new Vector3(0,1,1),//starting point
        new Vector3(16,1,1),//step
        new Vector3(16, 8, 1)};//final point
    bool finished;
    public static Text title {get;private set;} 
    public static Transform screen;

    void Start()
    {
        transform.FindChild("Return button").localScale = Vector3.zero;
        transform.FindChild("NetworkBackground").localScale = Vector3.zero;
        title = transform.FindChild("Title").GetComponent<Text>();
        title.text = "";
    }

    public static Text GetDisplay()
    {
        Text t = Instantiate(title);
        t.transform.SetParent(title.transform);
        t.rectTransform.SetParent(title.rectTransform);
        t.rectTransform.SetParent(title.transform);
        t.transform.SetParent(title.rectTransform);
        t.transform.localPosition = Vector3.zero;
        t.rectTransform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        t.transform.localRotation = new Quaternion(0, 0, 0, 1);
        t.alignment = TextAnchor.MiddleCenter;
        return t;
    }

    void OnGUI()
    {
        float elapsed = Time.time - start;
        int phase = Mathf.FloorToInt(elapsed * 2);
        if (phase < 2)
        {
            int from = opened ? phase : 2 - phase;
            int to = from + (opened ? 1 : -1);
            transform.FindChild("NetworkBackground").localScale = Vector3.Lerp(phases[from], phases[to], elapsed * 2 - phase);
        }
        if (elapsed > 1 && !finished)//just finished. will be called only once
        {
            if (opened)
            {
                transform.FindChild("Return button").localScale = new Vector3(-2, 2, 2);
                Networker.Connect();
            }
            else
            {
                transform.localScale = Vector3.zero;
            }
            finished = true;
        }
    }

    public void Open()
    {
        opened = true;
        start = Time.time;
        finished = false;
    }

    public void Close()
    {
        opened = false;
        start = Time.time;
        transform.FindChild("Return button").localScale = Vector3.zero;
        title.text = "";
        finished = false;
    }
}
