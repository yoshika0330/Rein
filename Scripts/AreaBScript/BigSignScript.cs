using UnityEngine;
using System.Collections;

public class BigSignScript : MonoBehaviour {
	WaterGimmick waGimmmick;	
	CloudGimmick clGimmick;

	public GameObject water;
	public GameObject cloud;
	public GameObject bigSignL;

	private bool boatMoveFlag = false;	//	ボートが動いていいかのフラグ
	private bool playerOnFlag = false;	//	ボートにプレイヤーが乗っているかのフラグ
	private bool boatStopFlag = false;	//	ボートを止めるフラグ

	private int boatResetCount = 0;	//	ボートが岸を超えないようにする時のカウント

	private GameObject player;

	Animator bigSignAnimaR;
	Animator bigSignAnimaL;

	void OnTriggerEnter(Collider boatEnd){
		//	ボートが岸に着きそうにになった時
		if (boatEnd.gameObject.name == "BoatEnd") {
			boatStopFlag = false;	//	ストップフラグを折る
			//	どっちの岸に着いたか判定
			switch (boatEnd.gameObject.tag) {
			//	左側だった場合ボートを右にちょっと戻す
			case "BoatEnd00":
				this.transform.position += new Vector3 (0.2f, 0, 0);
				break;
			//	右側だった場合ボートを左にちょっと戻す
			case "BoatEnd01":
				this.transform.position += new Vector3 (-0.2f, 0, 0);
				break;
			default:
				break;
			}
		}
	}

	void OnTriggerExit(Collider boatEnd){
		//	ボートが岸から少し離れてかつリセットのカウントが０の時
		if (boatEnd.gameObject.name == "BoatEnd" && boatResetCount == 0) {
			boatStopFlag = true;	//	ストップフラグを立てる
			boatResetCount++;	//	リセットのカウントアップ
		}
	}


	void OnCollisionEnter (Collision player){
		// プレイヤーが触れたら
		if (player.gameObject.name == "Player") {
			playerOnFlag = true;	//	フラグを立てる
		}
	}

	void OnCollisionExit (Collision player){
		if (player.gameObject.name == "Player") {
			playerOnFlag = false;	//	フラグを折る
		}
	}

	void Start () {
		waGimmmick = water.GetComponent<WaterGimmick> ();
		clGimmick = cloud.GetComponent<CloudGimmick> ();
		bigSignAnimaR = GetComponent<Animator> ();
		bigSignAnimaL = bigSignL.GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag("Player");

	}

	void Update () {
		//	巨大看板のギミック発動許可が出ていてギミックを触りながら下にスライドされたら
		if (GimmickController.Instance.bigSignGo == 1 && GimmickController.Instance.tapPositionDown == 1) {
			GimmickController.Instance.bigSignGAnima = true;	//	ギッミック発動時のアニメーションフラグを立てる
			bigSignAnimaR.SetBool("play" ,true);				//	巨大看板右のアニメーションを再生させる
			bigSignAnimaL.SetBool ("play", true);				//	巨大看板左のアニメーションを再生させる
			StartCoroutine ("animatorFalse");	
		}


		if (boatMoveFlag) {
			
			if (boatResetCount == 0) {
				boatStopFlag = false;
			}
				
			//	水ギミック触って右にスライドさせ更にボートが動ければ
			if (waGimmmick.tapRight == 1 && boatStopFlag == false) {
				this.transform.position += new Vector3 (0.03f, 0, 0);	//	右に移動
				//	プレイヤーが乗っていたら同じ量移動させる
				if (playerOnFlag) {
					player.transform.position += new Vector3 (0.03f, 0, 0);
				}	
			}
			//	水ギミック触って左にスライドさせ更にボートが動ければ
			else if (waGimmmick.tapLeft == 1 && boatStopFlag == false) {
				this.transform.position -= new Vector3 (0.03f, 0, 0);	//	左に移動
				//	プレイヤーが乗っていたら同じ量移動させる
				if (playerOnFlag) {
					player.transform.position -= new Vector3 (0.03f, 0, 0);
				}
			}
		}

		//	水ギミックが蒸発したら
		if (GimmickController.Instance.waterGimmickFlag) {
			this.transform.position -= new Vector3 (0, 0.01f, 0);	//	位置を水に合わせて下げる
		}
		//	雨が降って来たら
		if (GimmickController.Instance.cloudGimmickFlag) {
			this.transform.position += new Vector3 (0, 0.01f, 0);	//	位置を水に合わせて上げる
			//	プレイヤーが乗っていたら同じ量移動させる
			if (playerOnFlag) {
				player.transform.position += new Vector3 (0, 0.01f, 0);
			}
		}

		if (GimmickController.Instance.waterGimmickGo == 0){
			boatResetCount = 0;	//	リセットカウントの初期化
		}
	}
		

	private IEnumerator animatorFalse()
	{
		yield return new WaitForSeconds (2.5f);	//	アニメーションが終わるくらいの時間に調整
		this.GetComponent<Animator> ().enabled = false;	//	アニメーターを切る
		boatMoveFlag = true;	
		GimmickController.Instance.bigSignGAnima = false;	//	ギッミック発動時のアニメーションフラグを折る
	}
}
