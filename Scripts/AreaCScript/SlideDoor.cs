using UnityEngine;
using System.Collections;

public class SlideDoor : MonoBehaviour {

	ButtonA buAScript;
	ButtonB buBScript;

	Animator leftDoorAnima;
	Animator rightDoorAnima;

	public GameObject leftDoor;
	public GameObject rightDoor;
	public GameObject buttonA;
	public GameObject buttonB;
	public GameObject slideCol;

	// Use this for initialization
	void Start () {
		buAScript = buttonA.GetComponent<ButtonA> ();
		buBScript = buttonB.GetComponent<ButtonB> ();
		leftDoorAnima = leftDoor.GetComponent<Animator> ();
		rightDoorAnima = rightDoor.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (buAScript.openDoor == 1) {
			OpenDoor ();
		} else if (buBScript.openDoor == 1) {
			OpenDoor ();
		}
		else if(buAScript.openDoor == 0 && buBScript.openDoor == 0){
			slideCol.gameObject.SetActive (true);
			leftDoorAnima.SetBool ("Open", false);
			leftDoorAnima.SetBool ("Close", true);
			rightDoorAnima.SetBool ("Open", false);
			rightDoorAnima.SetBool ("Close", true);
		}
	
	}

	void OpenDoor(){
		slideCol.gameObject.SetActive (false);
		leftDoorAnima.SetBool ("Close", false);
		leftDoorAnima.SetBool ("Open", true);
		rightDoorAnima.SetBool ("Close", false);
		rightDoorAnima.SetBool ("Open", true);
	}

	void CloseDoor(){
		
	}
}
