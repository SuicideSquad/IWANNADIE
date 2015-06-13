using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;

public class NetworkServer : MonoBehaviour
{
    static Text t;
    static string roomname;
    static string defaulttext;
    static bool error;

    void Start()
    {
        t = GetComponentInChildren<Text>();
        defaulttext = t.text;
        roomname = "My room";
        error = false;
    }

    void OnGUI()
    {
        if (Network.isServer)
        {
            t.text = "Connected players:\n\n" + Network.connections.Length;
        }
        else if (error)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height - 150, 200, 100), "OK"))
            {
                error = false;
                t.text = defaulttext;
            }
        }
        else
        {
            roomname = GUI.TextArea(new Rect(Screen.width / 2 - 375 / 2, Screen.height / 2 - 10, 375, 20), roomname, 32);
            roomname = Regex.Replace(roomname, @"[^a-zA-Z0-9 ]", "");
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 200, 100), "Create room"))
            {
                t.text = "Please wait...";
                WebClient wc = new WebClient();
                string answer = "";
                try
                {
                    answer = wc.DownloadString("http://suicide-squad.esy.es/game_actions/create.php?name=" + roomname);
                }
                catch (Exception e)
                {
                    answer = e.ToString();
                }
                if (answer.IndexOf("ok") != 0)
                {
                    t.text = "Error :\t\t\n\n" + answer;
                    error = true;
                    return;
                }
                Network.InitializeSecurity();
                Network.InitializeServer(4, 25002, !Network.HavePublicAddress());
            }
        }
    }

    void OnApplicationQuit()
    {
        new WebClient().DownloadString("http://suicide-squad.esy.es/game_actions/quit.php?room=" + roomname);
    }
}
