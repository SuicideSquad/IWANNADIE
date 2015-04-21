using UnityEngine;
using System.Collections;

public class heart : MonoBehaviour
{
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
        if (c.name == "Player")
        {
            c.GetComponentInParent<NetworkPlayer>().HealthBonus(10);
            Destroy(gameObject);
        }
    }
}
