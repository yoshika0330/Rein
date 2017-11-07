using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	private bool onPlayer = false;
	private bool onRobo = false;
	private bool upFlag = false;
	private bool downFlag = false;
	private bool cameraOnFlag = false;

	public float downLine;
	public float upLine;

	public GameObject robo;

	private const string mainCamera = "MainCamera";

	// Use this for initialization
	void Start () {
	
	}

	void OnWillRenderObject(){
		if (Camera.current.tag == mainCamera) {
			cameraOnFlag = true;	//	カメラに写っていたらフラグを立てる
		}
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.tag == "Player") {
			onPlayer = true;
		} else if (collider.gameObject.tag == "Robo") {
			onRobo = true;
		}
	}

	void OnTriggerExit (Collider collider) {
		if (collider.gameObject.tag == "Player") {
			onPlayer = false;
		} else if (collider.gameObject.tag == "Robo") {
			onRobo = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (this.transform.position.y <= upLine) {
			upFlag = true;
		} else
			upFlag = false;

		if (this.transform.position.y > downLine) {
			downFlag = true;
		} 
		else 
			downFlag = false;
			
		if (cameraOnFlag) {
			if (GimmickManager.Instance.elevatorGo) {
				if (GimmickManager.Instance.tapPositionUP == 1 && upFlag) {
					this.transform.position += new Vector3 (0, 0.02f, 0);
					if (onPlayer)
						PlayerMove_C.Instance.transform.position += new Vector3 (0, 0.02f, 0);
					if (onRobo)
						robo.transform.position += new Vector3 (0, 0.02f, 0);
				} else if (GimmickManager.Instance.tapPositionDown == 1 && downFlag) {
					this.transform.position -= new Vector3 (0, 0.02f, 0);
					PlayerMove_C.Instance.moveFlag = false;
				}
			}
		}

		cameraOnFlag = false;
	
	}
}
