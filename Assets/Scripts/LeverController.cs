using UnityEngine;
using System.Collections;

public class LeverController : MonoBehaviour {

	public GameObject player;
	public GameObject myLight;

	internal bool position;

	// Use this for initialization
	void Start () {
		GetComponent<Animation>().CrossFade ("SwitchOff", 0.1f);
		myLight.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 ppos = player.transform.position;
		Vector3 lpos = transform.position;

		bool action = Input.GetKeyDown (KeyCode.E);

		if(action){
			float distance = Vector3.Distance (ppos, lpos);
			if(distance < 7){
				if(position){
					GetComponent<Animation>().CrossFade ("SwitchOff", 0.1f);
					myLight.SetActive (false);
				} else {
					GetComponent<Animation>().CrossFade ("SwitchOn", 0.1f);
					myLight.SetActive (true);
				}
				position = !position;
			}
		}
	
	}	
}
