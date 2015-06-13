using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
using System.Collections.Generic;
using System;
using System.Threading;

public class Networker : MonoBehaviour
{
    class room
    {
        public string name;
        public int population;

        public room(string name, int pop)
        {
            this.name = name;
            this.population = pop;
        }
    }

    static List<room> rooms = new List<room>();

    public static int length { get { return rooms.Count; } }

    public static int done = 0;

    static bool connecting;

    static public IEnumerator Connect()
    {
        connecting = true;
        while (connecting)
        {
            yield return null;
            Text disp = NetworkScreen.GetDisplay(NetworkScreen.screen, "Networker display");
            WebClient wc = new WebClient();
            rooms = new List<room>();
            yield return null;
            try
            {
                string list = wc.DownloadString("http://suicide-squad.esy.es/game_actions/list.php?void");
                NetworkScreen.title.text = "List of rooms";
                if (list.Length == 0)
                {
                    disp.text = "No rooms available";
                    yield break;
                }
                string[] lines = list.Split('\n');
                foreach (string line in lines)
                {
                    if (line == "")
                        continue;
                    string pop = "";
                    int i;
                    for (i = line.Length - 1; line[i] != ' '; i--)
                        pop = line[i] + pop;
                    rooms.Add(new room(line.Substring(0, i), Int32.Parse(pop)));
                }
            }
            catch (Exception e)
            {
                NetworkScreen.title.text = "Error";
                disp.text = "There was an error while parsing data from the server\n" + e;
                print(e);
                yield break;
            }
            NetworkScreen.title.text = "List of rooms";
            Destroy(disp.gameObject);

            Transform array = new GameObject().transform;
            array.SetParent(NetworkScreen.screen);
            array.name = "Networker display";
            array.localPosition = Vector3.zero;
            array.localRotation = Quaternion.identity;
            array.localScale = new Vector3(1, 1, 1);

            Text names = NetworkScreen.GetDisplay(array, "Rooms");
            names.transform.localPosition = new Vector3(-570, 260, 0);
            names.text = "Name of the room";

            Text players = NetworkScreen.GetDisplay(array, "Players");
            players.transform.localPosition = new Vector3(200, 260, 0);
            players.text = "Players";

            Text name, population;
            int n = 0;
            foreach (room r in rooms)
            {
                name = NetworkScreen.GetDisplay(array, "r" + n);
                name.transform.localPosition = new Vector3(-470, 160 - 100 * n);
                name.rectTransform.sizeDelta = new Vector2(600, 300);
                name.text = r.name;
                population = NetworkScreen.GetDisplay(array, "p" + n);
                population.transform.localPosition = new Vector3(260, 160 - 100 * n);
                population.text += r.population;
                n++;
            }
            done += 1;
            yield return new WaitForSeconds(15);
            if (connecting)
                Destroy(GameObject.Find("NetworkCanvas/Networker display"));
        }
    }

    static public IEnumerator Create()
    {
        NetworkScreen.state = NetworkScreen.State.Create;
        connecting = false;
        NetworkScreen.title.text = "Create a room";
        NetworkScreen.Clear();
        Destroy(GameObject.Find("NetworkCanvas/Networker display"));
        Text disp = NetworkScreen.GetDisplay(NetworkScreen.screen, "Networker display");
        disp.alignment = TextAnchor.MiddleCenter;
        disp.text = "Please wait...";
        WebClient wc = new WebClient();
        yield return null;
        string answer = "";
        try
        {
            answer = wc.DownloadString("http://suicide-squad.esy.es/game_actions/create.php?name=" + NetworkScreen.roomname);
        }
        catch (Exception e)
        {
            answer = e.ToString();
        }
        if (answer.IndexOf("ok") != 0)
        {
            NetworkScreen.title.text = "Error";
            disp.text = answer + "\n\n5";
            yield return new WaitForSeconds(1);
            disp.text = answer + "\n\n4";
            yield return new WaitForSeconds(1);
            disp.text = answer + "\n\n3";
            yield return new WaitForSeconds(1);
            disp.text = answer + "\n\n2";
            yield return new WaitForSeconds(1);
            disp.text = answer + "\n\n1";
            yield return new WaitForSeconds(1);
            Destroy(disp);
            Destroy(disp.gameObject);
            NetworkScreen.Restart();
            yield break;
        }
        Network.InitializeSecurity();
        Network.InitializeServer(4, 25002, !Network.HavePublicAddress());
        Network.Connect("localhost", 25002);
        while (true)
        {
            disp.text = "Connected players:\n\n" + Network.connections.Length;
            if (GUI.Button(new Rect(1200, 500, 100, 100), "Start game session"))
                print("truc");
            yield return null;
        }
    }

    static public IEnumerator Join(int roomnum)
    {
        NetworkScreen.state = NetworkScreen.State.Join;
        connecting = false;
        NetworkScreen.title.text = "Join a room";
        NetworkScreen.Clear();
        Destroy(GameObject.Find("NetworkCanvas/Networker display"));
        Text disp = NetworkScreen.GetDisplay(NetworkScreen.screen, "Networker display");
        disp.alignment = TextAnchor.MiddleCenter;
        disp.text = "Please wait...";
        WebClient wc = new WebClient();
        yield return null;
        string answer = "";
        try
        {
            answer = wc.DownloadString("http://suicide-squad.esy.es/game_actions/join.php?room=" + rooms[roomnum].name);
        }
        catch (Exception e)
        {
            answer = e.ToString();
        }
        if (answer.IndexOf("ok") != 0)
        {
            NetworkScreen.title.text = "Error";
            disp.text = answer + "\n\n5";
            yield return new WaitForSeconds(1);
            disp.text = answer + "\n\n4";
            yield return new WaitForSeconds(1);
            disp.text = answer + "\n\n3";
            yield return new WaitForSeconds(1);
            disp.text = answer + "\n\n2";
            yield return new WaitForSeconds(1);
            disp.text = answer + "\n\n1";
            yield return new WaitForSeconds(1);
            Destroy(disp);
            Destroy(disp.gameObject);
            NetworkScreen.Restart();
            yield break;
        }
        Network.Connect(answer.Substring(3), 25002);
    }
}
