using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	CanvasScript CS;

	public int beforeFlag = 0;
	public int nextFlag = 0;

	// Use this for initialization
	void Start () {
		CS = GameObject.Find ("Canvas").GetComponent<CanvasScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BeforeButtonPush(){
		beforeFlag = 1;	//	前ボタンが押されたらフラグを立てる
	}

	public void NextButtonPush(){
		nextFlag = 1;	//	次ボタンが押されたらフラグを立てる
	}

	public void ExitButtonPush(){
		CS.canvasOutFlag = 1;
		CS.canvasFlag = 0;
	}
}
