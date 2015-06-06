using UnityEngine;
using System.Collections;

public class Click : MonoBehaviour
{
    public enum Actions { move, play, newGame, load, exit, setKey, changeValue };

    public Actions action;

    [System.Serializable]
    public class Movement
    {
        public Vector3 moveTo;
        public Quaternion rotateTo;
        public float animationtime = 2f;
        public bool doubleMovement = false;
        public Vector3 via;
        public enum networkAction { DoNothing, Open, Close }
        public networkAction networkView;

        [System.NonSerialized]
        public bool moving;

        Transform cam;
        Transform networkBackground;
        Vector3 origin;
        Quaternion rotation;
        float time;

        public void Init(GameObject camera)
        {
            cam = camera.transform;
            origin = cam.position;
            rotation = cam.rotation;
            time = 0;
            moving = true;
            if (networkView == networkAction.Close)
            {
                GameObject.Find("NetworkCanvas").GetComponent<NetworkScreen>().Close();
                time = -1;
            }
        }

        public void Update()
        {
            time += Time.deltaTime;
            if (doubleMovement)
                if (time < animationtime / 2)
                    cam.position = Vector3.Lerp(origin, via, time * 2 / animationtime);
                else
                    cam.position = Vector3.Lerp(via, moveTo, time * 2 / animationtime - 1);
            else
                cam.position = Vector3.Lerp(origin, moveTo, time / animationtime);
            cam.rotation = Quaternion.Lerp(rotation, rotateTo, time / animationtime);
            if (time > animationtime)
            {
                moving = false;
                if (networkView == networkAction.Open)
                    GameObject.Find("NetworkCanvas").GetComponent<NetworkScreen>().Open();
            }
        }
    }

    public Movement movement;

    void Update()
    {
        if (movement.moving)
            movement.Update();
    }

    void OnMouseDown()
    {
        switch (action)
        {
            case Actions.move:
                movement.Init(GameObject.Find("Main Camera"));
                break;
            case Actions.play:
                //do something
                break;
            case Actions.newGame:
                Application.LoadLevel("ch1lvl1");
                break;
            case Actions.load:
                Application.LoadLevel(Parameters.level);
                break;
            case Actions.exit:
                Application.Quit();
                break;
            case Actions.setKey:
                //
                break;
            case Actions.changeValue:
                //
                break;
        }
    }
}
