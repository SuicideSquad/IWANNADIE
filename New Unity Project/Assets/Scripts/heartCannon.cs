using UnityEngine;
using System.Collections;

public class heartCannon : MonoBehaviour
{
    public GameObject heartPrefab;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject heartInstance;
            heartInstance = Instantiate(heartPrefab, transform.position + GetComponentInChildren<Camera>().transform.forward * 3, Quaternion.identity) as GameObject;
            heartInstance.GetComponent<Rigidbody>().velocity = GetComponentInChildren<Camera>().transform.forward * 50;
        }
    }
}
