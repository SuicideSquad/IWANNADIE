using UnityEngine;
using System.Collections;

public class blade : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 20f);
    }

    void Update()
    {
        transform.Rotate(-Vector3.up, Time.deltaTime * 50);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.name == "Network_Player")
        {
            c.GetComponentInParent<NetworkPlayer>().Cut();
            Destroy(gameObject);
        }
    }
}
