using UnityEngine;
using System.Collections;

public class SignScript : MonoBehaviour {

	TextController texCon;
	CanvasScript CS;
	GimmickController gimiCon;

	[HideInInspector]
	public int canvasFlag = 0;
	public int signDownFlag = 0;
	public int signUpFlag = 0;
	public int textFlag = 1;
	public int signNo;	//	看板識別用のナンバー

	public GameObject gimmickControl;
	public GameObject textController;

	//	看板に触れたら
	void OnTriggerEnter(Collider kannban){
		CS.HitkanNumber = signNo;	//	看板のそれぞれのナンバーを取得
		if(textFlag == 1)
			CS.canvasFlag = 1;			//	キャンバスを表示
	}

	//	看板から離れたら
	void OnTriggerExit(Collider kannban){
		CS.canvasFlag = 0;
		texCon.textOne = 0;
		texCon.currentLine = -1;
	}
	// Use this for initialization
	void Start () {
		gimiCon = gimmickControl.GetComponent<GimmickController> ();
		texCon = textController.GetComponent<TextController> ();
		CS = GameObject.Find ("Canvas").GetComponent<CanvasScript> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (gimiCon.signGimmickGo == 1 && gimiCon.tapPositionDown == 1) {
			signDownFlag = 1;
		}
		if (gimiCon.signGimmickGo == 1 && gimiCon.tapPositionUP== 1) {
			signUpFlag = 1;
		}

		if (gimiCon.signGimmickGo == 0) {
			signDownFlag = 0;
			signUpFlag = 0;
		}
			

	}
}
