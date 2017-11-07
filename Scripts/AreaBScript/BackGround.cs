using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackGround : MonoBehaviour {

	private float alphaNoon = 1f;	//	昼の背景の初期アルファ値
	private float alphaEvening = 1f;	//	夕方の背景の初期アルファ値
	private float speed =0.005f;	//	フェイドさせる時のスピード

	public GameObject[] backGround;	//	背景画像をここに格納

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//	プレイヤーからチェンジ判定１が来たら背景を透過して夕方の背景にする
		if (PlayerMove.Instance.change == 1) {
			backGround [0].GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, alphaNoon);
			alphaNoon -= speed;
		}
		//	プレイヤーからチェンジ判定２が来たら背景を透過して夜の背景にする
		else if (alphaNoon <= 0f && PlayerMove.Instance.change == 2) {
			backGround [1].GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, alphaEvening);
			alphaEvening -= speed;
		}
	}
}
