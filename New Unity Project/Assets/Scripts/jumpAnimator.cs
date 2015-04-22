using UnityEngine;
using System.Collections;

public class jumpAnimator : MonoBehaviour
{
    float life = Mathf.PI / 2;
    void Update()
    {
        life += Time.deltaTime;
        transform.Translate(Vector3.forward * Mathf.Sin(life * 4) / 100);
    }
}
