using UnityEngine;
using System.Collections;

public class cannonPickable : MonoBehaviour
{
    public GameObject attaches;
    public GameObject shoots;

    void Start()
    {
        Destroy(gameObject, 20f);
    }

    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 100);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.name == "Network_Player")
        {
            //attach cannon to camera
            GameObject cannon = Instantiate(attaches, Vector3.zero, Quaternion.identity) as GameObject;
            cannon.transform.parent = c.transform.Find("Main Camera");
            cannon.transform.localPosition = new Vector3(0.5f, -2.5f, -1f);
            cannon.transform.localEulerAngles = new Vector3(-100f, 0, 0);
            //attach heartCannon.cs
            heartCannon hc = cannon.AddComponent<heartCannon>();
            hc.shoots = this.shoots;
            //program destruction of cannon
            Destroy(cannon, 30f);
            //self destruction
            Destroy(gameObject);
        }
    }
}
