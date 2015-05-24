using UnityEngine;
using System.Collections;

public class PickableBlock : MonoBehaviour
{
    public bool canPick = true;

    Transform block;
    bool hasBlock = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (!hasBlock)
            {
                RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, 3f);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.name == "Pickable Block")
                    {
                        hasBlock = true;
                        block = hit.transform;
                        break;
                    }
                }
            }
            else
                hasBlock = false;
        }
    }

    void FixedUpdate()
    {
        if (hasBlock)
            block.position = transform.position + transform.forward * 3f;
    }
}