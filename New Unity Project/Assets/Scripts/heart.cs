using UnityEngine;
using System.Collections;

public class heart : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 100);
    }
}
