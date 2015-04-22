using UnityEngine;
using System.Collections;

public class jumpBonus : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 20f);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.name == "Network_Player")
        {
            c.GetComponentInParent<NetworkPlayer>().JumpBonus();
            Destroy(gameObject);
        }
    }
}
