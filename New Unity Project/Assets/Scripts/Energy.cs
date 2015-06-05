using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Energy : MonoBehaviour
{

    public Slider Speed;
    public Image Fill;
    public Image Background;
    private bool run = true;

    // Use this for initialization
    void Start()
    {
        Fill.enabled = false;
        Background.enabled = false;
        Fill.color = Color.grey;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && run)
        {
            Fill.enabled = true;
            Background.enabled = true;
            Speed.value -= 0.003F;
            if (Speed.value == 0)
            {
                run = false;
				GameObject.Find("Player").GetComponent<Attributes>().canRun=false;
            }
        }
        else
        {
            if (Speed.value != 1)
            {
                Speed.value += 0.008F;
                if (Speed.value >= 0.6F)
                {
					GameObject.Find("Player").GetComponent<Attributes>().canRun= true;
                    run = true;
                }
            }
            else
            {
                Fill.enabled = false;
                Background.enabled = false;

            }
        }
    }
}
