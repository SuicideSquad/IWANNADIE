using UnityEngine;
using System.Collections;

public class heartRenderer : MonoBehaviour
{
    float life = 0;
    void Update()
    {
        life += Time.deltaTime;
        transform.Translate(Vector3.forward * Mathf.Sin(life * 4) / 100);
    }
}
