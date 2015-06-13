using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Parameters : MonoBehaviour
{
    static System.Random rand = new System.Random();
    public static float soundlevel;
    public static float graphicslevel;

    public static KeyCode forward, backward, left, right, crawl, jump, run, interact;

    public static string pseudonym;

    public static int level;

    public static void Load()
    {
        if (File.Exists("IWANNADIE.dat"))
        {
            try
            {
                StreamReader save = new StreamReader("IWANNADIE.dat");

                string read = save.ReadLine();
                soundlevel = float.Parse(read);
                read = save.ReadLine();
                graphicslevel = float.Parse(read);

                read = save.ReadLine();
                forward = (KeyCode)Enum.Parse(typeof(KeyCode), read);
                read = save.ReadLine();
                backward = (KeyCode)Enum.Parse(typeof(KeyCode), read);
                read = save.ReadLine();
                left = (KeyCode)Enum.Parse(typeof(KeyCode), read);
                read = save.ReadLine();
                right = (KeyCode)Enum.Parse(typeof(KeyCode), read);
                read = save.ReadLine();
                crawl = (KeyCode)Enum.Parse(typeof(KeyCode), read);
                read = save.ReadLine();
                jump = (KeyCode)Enum.Parse(typeof(KeyCode), read);
                read = save.ReadLine();
                run = (KeyCode)Enum.Parse(typeof(KeyCode), read);
                read = save.ReadLine();
                interact = (KeyCode)Enum.Parse(typeof(KeyCode), read);

                pseudonym = save.ReadLine();

                byte key = (byte)save.Read();
                byte level_ciphered = (byte)save.Read();
                level = (int)((level_ciphered - key) ^ key);

                save.Close();
                return;
            }
            catch (FormatException)
            {
                print("Invalid save file. Will be regenerated, but all data will be lost.");
            }
        }
        soundlevel = 1f;
        graphicslevel = 1f;

        forward = KeyCode.Z;
        backward = KeyCode.S;
        left = KeyCode.Q;
        right = KeyCode.D;

        crawl = KeyCode.X;
        jump = KeyCode.Space;
        run = KeyCode.LeftShift;
        interact = KeyCode.E;

        pseudonym = "Player" + new System.Random().Next(100000);

        level = 1;

        Save();
    }

    public static bool GetInput(string name)
    {
        KeyCode key = KeyCode.None;
        switch (name)
        {
            case "forward":
                key = forward;
                break;
            case "backward":
                key = backward;
                break;
            case "left":
                key = left;
                break;
            case "right":
                key = right;
                break;
            case "crawl":
                key = crawl;
                break;
            case "jump":
                key = jump;
                break;
            case "run":
                key = run;
                break;
            case "interact":
                key = interact;
                break;
        }
        return Input.GetKeyDown(key);
    }

    public static void Save()
    {
        StreamWriter save = new StreamWriter("IWANNADIE.dat", false);

        save.WriteLine(soundlevel);
        save.WriteLine(graphicslevel);

        save.WriteLine(forward);
        save.WriteLine(backward);
        save.WriteLine(left);
        save.WriteLine(right);

        save.WriteLine(crawl);
        save.WriteLine(jump);
        save.WriteLine(run);
        save.WriteLine(interact);

        save.WriteLine(pseudonym);

        byte key = (byte)rand.Next();
        byte level_ciphered = (byte)(((byte)level) ^ key);
        save.Write((char)key);
        save.Write((char)(level_ciphered + key));

        save.Close();
    }

    public static void LevelChanged()
    {
        level = Application.loadedLevel;
        Save();
    }
}
