using UnityEngine;
using System.Collections;

public class heartCannon : MonoBehaviour
{
    public GameObject shoots;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Transform p = transform.parent;
            GameObject heartInstance = Instantiate(shoots, p.position + p.forward * 3, Quaternion.identity) as GameObject;
            heartInstance.GetComponent<Rigidbody>().velocity = transform.parent.forward * 50;
        }
    }
}
