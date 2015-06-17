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
    static bool finished;
    public static Text title;
    public static Transform screen;
    static Text pseudo, create;
    public static string playername, roomname;

    public enum State { List, Create, Join };

    public static State state;

    public Transform networkPlayer;

    void Start()
    {
        Parameters.Load();
        /*#################*/
        screen = transform;
        transform.FindChild("Return button").localScale = Vector3.zero;
        transform.FindChild("NetworkBackground").localScale = Vector3.zero;
        title = transform.FindChild("Title").GetComponent<Text>();
        title.text = "";
        playername = Parameters.pseudonym;
        roomname = "My room";
        state = State.List;
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
        if (state == State.List)
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
            if (finished && opened)//finished
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
                    playername = Regex.Replace(playername, @"[^a-zA-Z0-9 -_]", "");
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
                    roomname = Regex.Replace(roomname, @"[^a-zA-Z0-9 -_]", "");
                    if (GUI.Button(new Rect(1650, 691, 100, 50), "Create"))
                        StartCoroutine(Networker.Create());
                }
                for (int n = 0; n < Networker.length; n++)
                    if (GUI.Button(new Rect(1200, 250 + n * 100, 100, 90), "Join this room"))
                        StartCoroutine(Networker.Join(n));
            }
        }
        if (Network.isServer)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 250, Screen.height / 2 + 100, 200, 100), "End server"))
            {
                Network.Disconnect();
                Networker.waiting = false;
            }
            if (Network.connections.Length > 0)
                if (GUI.Button(new Rect(Screen.width / 2 + 50, Screen.height / 2 + 100, 200, 100), "Start game session"))
                    Networker.waiting = false;
            if (!Networker.waiting && Networker.map == "")
            {
                if (Network.connections.Length > 4)
                    Networker.map = "networklevel2";
                else
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - 250, Screen.height / 2 + 100, 200, 100), "Play on map 1"))
                        Networker.map = "networklevel1";
                    if (GUI.Button(new Rect(Screen.width / 2 + 50, Screen.height / 2 + 100, 200, 100), "Play on map 2"))
                        Networker.map = "networklevel2";
                }
            }
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
        state = State.List;
        StopAllCoroutines();
        Destroy(GameObject.Find("NetworkCanvas/Networker display"));
        try
        {
            Destroy(pseudo.gameObject);
        }
        catch (System.Exception) { }
        try
        {
            Destroy(create.gameObject);
        }
        catch (System.Exception) { }
    }

    public static void Clear()
    {
        Destroy(pseudo.gameObject);
        Destroy(create.gameObject);
    }

    public static void Restart()
    {
        Destroy(GameObject.Find("NetworkCanvas/Networker display"));
        Networker.done = 0;
        state = State.List;
        finished = false;
    }

    void OnApplicationQuit()
    {
        new System.Net.WebClient().DownloadString("http://suicide-squad.esy.es/game_actions/quit.php?room=" + roomname);
        Parameters.pseudonym = playername;
        Parameters.Save();
    }

    [RPC]
    void LoadMap(string map, int id)
    {
        Application.LoadLevel(map);
        Transform spawn = GameObject.Find("Spawn" + id).transform;
        Network.Instantiate(networkPlayer, spawn.position, spawn.rotation, 0).name = playername;
    }
}
