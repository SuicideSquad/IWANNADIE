using UnityEngine;
using System.Collections;

public class heartRenderer : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.forward * Mathf.Sin(Time.time * 4) / 100);
    }
}
