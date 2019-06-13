using UnityEngine;
using System.Collections;

public class MyCameraController : MonoBehaviour {

	public GameObject player;
	public float thirdradius;
	public float firstradius;


	private Vector3 thirdposition;
	private Vector3 firstposition;
	private Vector3 thirdangle;
	private Vector3 firstangle;
	private bool view;

	// Use this for initialization
	void Start () {
		view = true;
	}
	
	// Update is called once per frame
	void Update () {
		updateFirstPerson ();
		updateThirdPerson ();

		if(Input.GetKeyDown (KeyCode.Q)){
			view = !view;
		}

		if(view){
			transform.eulerAngles = thirdangle;
			transform.position = thirdposition;
		} else {
			transform.eulerAngles = firstangle;
			transform.position = firstposition;
		}


	}

	void updateThirdPerson(){
		Vector3 playerpos = player.transform.position;
		float playerangle = player.transform.eulerAngles.y;

		thirdangle = new Vector3 (30f, playerangle, 0f);

		float offx = thirdradius * Mathf.Sin (playerangle * Mathf.Deg2Rad);
		float offz = thirdradius * Mathf.Cos (playerangle * Mathf.Deg2Rad);

		thirdposition = new Vector3 (playerpos.x - offx, playerpos.y + 7f, playerpos.z - offz);

	}

	void updateFirstPerson(){
		Vector3 playerpos = player.transform.position;
		float playerangle = player.transform.eulerAngles.y;

		firstangle = new Vector3 (0f, playerangle, 0f);

		float offx = firstradius * Mathf.Sin (playerangle * Mathf.Deg2Rad);
		float offz = firstradius * Mathf.Cos (playerangle * Mathf.Deg2Rad);
		
		firstposition = new Vector3 (playerpos.x + offx, playerpos.y + 4.5f, playerpos.z + offz);
	}

}
