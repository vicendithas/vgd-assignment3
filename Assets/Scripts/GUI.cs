using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {

	public GUIText instructions;
	public GUIText countText;
	public GUIText winText;
	public GUIText timerText;

	//public GameObject lever1;
	//public GameObject lever2;
	//public GameObject lever3;

	private int score;
	private float timer;
	private bool win;

	private LeverController[] levers;

	// Use this for initialization
	void Start () {

		levers = FindObjectsOfType<LeverController>();

		setInstructions ();
		winText.text = "";
		setCountText ();

		timer = 0f;
		score = 0;
		win = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		score = 0;
		
		foreach (LeverController lever in levers)
		{
			if(lever.position){
				score++;
			}
		}

		if(score == 3){
			winText.text = "YOU WIN!";
			win = true;
		}

		if(!win){
			timer += Time.deltaTime;
		}

		setTimerText ();

		setCountText ();

	}

	void setInstructions(){
		string goal = "Instructions: Turn on all the levers!";
		string msg0 = "\n\nControls";
		string dash = "\n------------------------";
		string msg1 = "\nArrow/WASD: Move/Rotate Player";
		string msg2 = "\nShift (hold): Sprint";
		string msg3 = "\nSpace: Jump";
		string msg4 = "\nQ: Change View";
		string msg5 = "\nE: Action";
		string msg6 = "\nU: Elevator up";
		string msg7 = "\nJ: Elevator down";
		string msg8 = "\nR: Restart game";
		
		instructions.text = goal + msg0 + dash + msg1 + msg2 + msg3 + msg4 + msg5 + msg6 + msg7 + msg8;
	}

	void setCountText(){
		countText.text = "Score: " + score;
	}

	void setTimerText(){
		timerText.text = "Timer: " + timer.ToString ("#0.00");
	}
}
