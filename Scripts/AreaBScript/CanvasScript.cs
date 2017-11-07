using UnityEngine;
using System.Collections;

public class CanvasScript : MonoBehaviour {


	public GameObject kanban;
	public int canvasFlag = 0;
	public int canvasOutFlag = 0;
	public int HitkanNumber = 0;
	public int sceneNo = 1;

	// Use this for initialization
	void Start () {
		GetComponent<Canvas>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (canvasFlag == 1) {
			GetComponent<Canvas> ().enabled = true;	//	キャンバスを表示
			if (sceneNo == 0) {
				PlayerMove_A.Instance.moveFlag = false;	//	キャラの動きを止める
			} else if (sceneNo == 1) {
				PlayerMove.Instance.moveFlag = false;	//	キャラの動きを止める
			} else if (sceneNo == 2) {
				PlayerMove_end.Instance.moveFlag = false;
				Debug.Log ("aaaa");
			} else if (sceneNo == 3) {
				PlayerMove_C.Instance.moveFlag = false;
			}
		}
		if (canvasOutFlag == 1) {
			GetComponent<Canvas> ().enabled = false;	//	キャンバスを隠す
			canvasOutFlag = 0;
			if (sceneNo == 0) {
				PlayerMove_A.Instance.moveFlag = true;//	キャラを動かす
			} else if (sceneNo == 1) {
				PlayerMove.Instance.moveFlag = true;	//	キャラの動きを止める
			} else if (sceneNo == 2) {
				PlayerMove_end.Instance.moveFlag = true;
			} else if (sceneNo == 3) {
				PlayerMove_C.Instance.moveFlag = true;
			}
		}
	}
}
