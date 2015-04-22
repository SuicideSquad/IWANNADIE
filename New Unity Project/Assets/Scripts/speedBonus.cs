using UnityEngine;
using System.Collections;

public class speedBonus : MonoBehaviour
{
    float life = 0;
    void Start()
    {
        Destroy(gameObject, 20f);
    }

    void Update()
    {
        life += Time.deltaTime;
        transform.Rotate(Vector3.up, Mathf.Sin(life * 5) * 2);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.name == "Network_Player")
        {
            c.GetComponentInParent<NetworkPlayer>().SpeedBonus();
            Destroy(gameObject);
        }
    }
}
