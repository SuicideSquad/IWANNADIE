using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class blade : MonoBehaviour
{
	bool damage = false;
	public Slider healthBar;
	void Start(){

	}
    void Update()
    {
		if (damage)
			healthBar.value -= 0.333F* Time.deltaTime;
    }

	void OnTriggerStay(Collider c){
		if (Input.GetKey (KeyCode.E)) {
			damage = true;
			GetComponentInChildren<Renderer>().enabled = false;
		}
	}
}
