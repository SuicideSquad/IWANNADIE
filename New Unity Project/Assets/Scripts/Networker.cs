using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
using System.Collections.Generic;
using System;

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

    static public void Connect()
    {
        NetworkScreen.title.text = "Connecting...";
        Text disp = NetworkScreen.GetDisplay();
        disp.transform.name = "Networker display";
        disp.rectTransform.SetParent(NetworkScreen.screen);
        try
        {
            WebClient wc = new WebClient();
            string list = wc.DownloadString("http://suicide-squad.esy.es/game_actions/list.php?void");
            if (list.Length == 0)
            {
                NetworkScreen.title.text = "List of rooms";
                disp.text = "No rooms available";
            }
            else
            {
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
                NetworkScreen.title.text = "List of rooms";
                disp.text = "";
                foreach (room r in rooms)
                    disp.text += r.name + "\t" + r.population + "\n";
            }
        }
        catch (Exception)
        {
            NetworkScreen.title.text = "Error";
            disp.text = "There was an error while parsing data from the server";
            return;
        }
    }
}
