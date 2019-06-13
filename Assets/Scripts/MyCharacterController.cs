using UnityEngine;
using System.Collections;

public class MyCharacterController : MonoBehaviour {


	public float rotatespeed;
	public float speed;
	public float jumpspeed;

	private bool walking;
	private bool running;
	private bool actioning;
	private bool moving;
	private bool idle;
	private bool jumping;
	private int modifier;
	private bool onground;

	// Use this for initialization
	void Start () {
		GetComponent<Animation>().GetClip ("Idle").wrapMode = WrapMode.Loop;
		GetComponent<Animation>().GetClip ("Walk").wrapMode = WrapMode.Loop;
		GetComponent<Animation>().GetClip ("Action").wrapMode = WrapMode.Once;
		GetComponent<Animation>().GetClip ("Jump").wrapMode = WrapMode.Once;
	
		modifier = 1;

		jumping = false;
		actioning = false;
		onground = true;

		GetComponent<Rigidbody>().freezeRotation = true;
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.R)){
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		}

		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		moving = (Mathf.Abs (vertical) > 0.5f);

		if(!moving)
		{
			running = false;
			walking = false;
			idle = true;
		}

		bool shift = Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift);

		if(moving){
			idle = false;
			if(shift){
				walking = false;
				running = true;
			} else {
				walking = true;
				running = false;
			}
		}
		
		if(Input.GetKey (KeyCode.E) && !jumping)
		{
			GetComponent<Animation>().CrossFade ("Action", 0.1f);
		}

		if(Input.GetKey(KeyCode.Space) && !actioning && onground){
			Vector3 currvel = GetComponent<Rigidbody>().velocity;
			currvel.y = jumpspeed;
			GetComponent<Rigidbody>().velocity = currvel;
			onground = false;
			//animation.CrossFade("Jump", 0.1f);
		}
		
		actioning = GetComponent<Animation>().IsPlaying("Action");
		jumping = GetComponent<Animation>().IsPlaying ("Jump");

		if(!actioning && !jumping && walking)
		{
			modifier = 1;
			GetComponent<Animation>()["Walk"].speed = 1;
			GetComponent<Animation>().CrossFade ("Walk", 0.5f);
		}

		if(!actioning && !jumping && running)
		{
			modifier = 2;
			GetComponent<Animation>()["Walk"].speed = 2;
			GetComponent<Animation>().CrossFade ("Walk", 0.5f);
		}

		if(!actioning && !jumping && idle){
			GetComponent<Animation>().CrossFade ("Idle", 0.5f);
		}



		Vector3 rotation = new Vector3 (0f, horizontal * rotatespeed * modifier * Time.deltaTime, 0f);
		transform.Rotate (rotation);

		float angle = transform.rotation.eulerAngles.y;
		float movex = modifier * speed * vertical * Mathf.Sin (angle * Mathf.Deg2Rad) * Time.deltaTime;
		float movez = modifier * speed * vertical * Mathf.Cos (angle * Mathf.Deg2Rad) * Time.deltaTime;

		Vector3 currentpos = transform.position;

		transform.position = new Vector3 (currentpos.x + movex, currentpos.y, currentpos.z + movez);

		printStatus ();

	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag.Equals ("Ground")){
			onground = true;
		}
	}

	void printStatus(){

		//print("Walking:" + walking + "\tRunning:" + running + "\tIdle:"+ idle + "\tActioning:"+ actioning + "\tJumping:" + jumping);
		//print ("On Ground:" + onground);
	}
}
