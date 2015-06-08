using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;
using System.Text.RegularExpressions;

public class NetworkScreen : MonoBehaviour
{
    bool opened = false;
    float start = -1;
    Vector3[] phases = new Vector3[]{
        new Vector3(0,1,1),//starting point
        new Vector3(16,1,1),//step
        new Vector3(16, 8, 1)};//final point
    bool finished;
    public static Text title;
    public static Transform screen;
    Text pseudo, create;
    string playername, roomname;
    System.Random r = new System.Random();

    void Start()
    {
        screen = transform;
        transform.FindChild("Return button").localScale = Vector3.zero;
        transform.FindChild("NetworkBackground").localScale = Vector3.zero;
        title = transform.FindChild("Title").GetComponent<Text>();
        title.text = "";
        playername = "Player" + r.Next(100000);
        roomname = "My room";
    }

    public static Text GetDisplay(Transform parent, string name)//generates a Text gameobject in the middle of the screen
    {
        Text t = Instantiate(title);
        t.name = name;
        Transform rect = t.transform;
        rect.SetParent(parent);
        rect.localPosition = Vector3.zero;
        rect.localRotation = Quaternion.identity;
        rect.localScale = new Vector3(1, 1, 1);
        t.text = "";
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
                transform.FindChild("Return button").localScale = new Vector3(-25, 10, 10);
                title.text = "Connecting...";
                StartCoroutine(Networker.Connect());
            }
            else
            {
                transform.FindChild("NetworkBackground").localScale = Vector3.zero;
            }
            finished = true;
        }
        if (finished && opened)
        {
            if (Networker.length > 0)
            {
                if (pseudo == null)
                {
                    pseudo = GetDisplay(transform, "Pseudo");
                    pseudo.transform.localPosition = new Vector3(-580, -260, 0);
                    pseudo.text = "Enter your name: ";
                }
                playername = GUI.TextArea(new Rect(520, 707, 375, 20), playername, 32);
                playername = Regex.Replace(playername, @"[^a-zA-Z0-9 ]", "");
            }
            if (Networker.done == 1)//only once
            {
                create = GetDisplay(transform, "Create");
                create.transform.localPosition = new Vector3(200, -260, 0);
                create.text = "Create a room: ";
                Networker.done = 2;
            }
            if (Networker.done > 0)
            {
                roomname = GUI.TextArea(new Rect(1250, 707, 375, 20), roomname, 32);
                roomname = Regex.Replace(roomname, @"[^a-zA-Z0-9 ]", "");
                if (GUI.Button(new Rect(1650, 691, 100, 50), "Create"))
                    print("stuff");
            }
            for (int n = 0; n < Networker.length; n++)
                if (GUI.Button(new Rect(1200, 250 + n * 100, 100, 90), "Join this room"))
                    Networker.Join(n);
        }
    }

    public void Open()
    {
        opened = true;
        start = Time.time;
        finished = false;
        Networker.done = 0;
    }

    public void Close()
    {
        opened = false;
        start = Time.time;
        transform.FindChild("Return button").localScale = Vector3.zero;
        title.text = "";
        finished = false;
        StopCoroutine(Networker.Connect());
        Destroy(GameObject.Find("NetworkCanvas/Networker display"));
        Destroy(GameObject.Find("NetworkCanvas/Pseudo"));
        Destroy(GameObject.Find("NetworkCanvas/Create"));
    }
}
