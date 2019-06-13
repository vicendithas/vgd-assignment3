using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour {

	public GameObject player;
	public GameObject door;
	public float speed;
	public float doorspeed;

	private int floor;
	private Vector4 limits;
	private bool onelevator;
	private float targetheight;
	private bool dooropen;

	// Use this for initialization
	void Start () {

		GetComponent<Rigidbody>().freezeRotation = true;

		floor = 1;
		dooropen = true;

		limits = new Vector4 (0, 0, 0, 0);
		limits.x = transform.position.x - transform.localScale.x / 2;
		limits.y = transform.position.x + transform.localScale.x / 2;
		limits.z = transform.position.z - transform.localScale.z / 2;
		limits.w = transform.position.z + transform.localScale.z / 2;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 ppos = player.transform.position;

		bool up = Input.GetKeyDown (KeyCode.U);
		bool down = Input.GetKeyDown (KeyCode.J);

		onelevator = (ppos.x > limits.x) && (ppos.x < limits.y) && (ppos.z > limits.z) && (ppos.z < limits.w);
	
		if(onelevator && up){
			floor = Mathf.Min (3, floor + 1);
		}

		if(onelevator && down){
			floor = Mathf.Max (1, floor - 1);
		}

		Vector3 epos = transform.position;
		targetheight = ((floor - 1) * 10f) - 0.5f;
		float difference = targetheight - epos.y;
		float direction = difference / Mathf.Abs (difference);

		if(Mathf.Abs(difference) > 0.1){
			dooropen = false;
		} else {
			dooropen = true;
		}

		float doordiff = updateDoor ();

		if(!dooropen && (doordiff < 0.1)){
			transform.position = new Vector3 (epos.x, epos.y + (direction * speed * Time.deltaTime), epos.z);
		}


		printStatus ();
	
	}

	float updateDoor(){
		Vector3 dpos = door.transform.position;

		float openpos = 0f;
		float closedpos = 10f;

		float doortarget;
		if(dooropen){
			doortarget = openpos;
		} else {
			doortarget = closedpos;
		}

		float difference = doortarget - door.transform.position.z;
		float direction = difference / Mathf.Abs (difference);

		if(Mathf.Abs (difference) > 0.1){
			door.transform.position = new Vector3(dpos.x, dpos.y, dpos.z + (direction * doorspeed * Time.deltaTime));
		}

		return difference;
	
	}

	void printStatus(){
		//print ("On Elevator:" + onelevator + "\tTarget Height:" + targetheight);
	}


}
