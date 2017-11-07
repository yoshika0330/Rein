using UnityEngine;
using System.Collections;

public class ButtonA : MonoBehaviour {

	Animator buttonAnima;

	public int openDoor = 0;
	public GameObject slideCol;

	private bool onPlayer = false;
	private bool onRobo = false;

	void Start () {
		buttonAnima = GetComponent<Animator> ();
	}

	void OnCollisionEnter (Collision collision) {
		//	上にプレイヤーかロボットが乗ったかを判定
		if(collision.gameObject.tag == "Player")
			onPlayer = true;
		else if(collision.gameObject.tag == "Robo")
			onRobo = true;
	}

	void OnCollisionExit (Collision collision) {
		//	プレイヤーかロボットが離れたか判定
		if (collision.gameObject.tag == "Player")
			onPlayer = false;
		else if (collision.gameObject.tag == "Robo")
			onRobo = false;
	}
		
	void Update () {

		if(onPlayer || onRobo){
			buttonAnima.SetBool ("playUp", false);
			buttonAnima.SetBool ("playDown", true);	//	ボタンを下げるアニメーションを再生
			openDoor = 1;
		}

		else if(onPlayer == false && onRobo == false){
			openDoor = 0;
			buttonAnima.SetBool ("playDown", false);
			buttonAnima.SetBool ("playUp", true);	//	ボタンを上げるアニメーションを再生
		}

	}
}