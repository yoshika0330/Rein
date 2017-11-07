using UnityEngine;
using System.Collections;

public class BoatAnimation : MonoBehaviour {

	BoatScript boScript;
	CloudGimmick clGimmick;

	Animator boatAnima;

	public GameObject boatFront;
	public GameObject cloudGimmick;

	public int boatAnimationFlag = 0;
	[HideInInspector]
	public int playAnimation = 0;

	void Start () {
		boScript = boatFront.GetComponent<BoatScript> ();
		clGimmick = cloudGimmick.GetComponent<CloudGimmick> ();
		boatAnima = GetComponent<Animator> ();

	}

	void Update () {
		if (boScript.breakBoat == 1) {

			//	水位が下がったら水面の位置に合わせる
			if (GimmickController.Instance.waterGimmickFlag) {
				transform.position -= new Vector3 (0, 0.01f, 0);
			}
			//	雨が降って水位が上がったら水面の位置に合わせる
			else if (GimmickController.Instance.cloudGimmickFlag) {
				transform.position += new Vector3 (0, 0.01f, 0);
			}

			//	雲のアルファ値が０の時（水位が一番上がっている時）
			if (clGimmick.alpha == 0) {
				if (GimmickController.Instance.boatAnimationGo == 1 && GimmickController.Instance.tapPositionUP == 1) {
					this.GetComponent<Animator> ().enabled = true;			//	ボートの後ろの部分のアニメーターを有効化
					boatFront.GetComponent<Animator> ().enabled = true;		//	ボートの前の部分のアニメーターを有効化
					playAnimation = 1;	//	アニメーションの再生フラグを立てる
				}
			}
		}
		if (playAnimation == 1) {
			boatAnimationFlag = 1;
			boatAnima.SetBool ("BoatHeal", true);	//	ボートの後ろの部分の修復アニメーションを開始
			boScript.boatMoveFlag = 1;				//	ボートのムーブフラグを立てる
			StartCoroutine ("animatorFalse");	
		}

	}
	private IEnumerator animatorFalse()
	{
		yield return new WaitForSeconds(3.8f);	//	アニメーションが終わるくらいの時間に調整
		this.GetComponent<Animator> ().enabled = false;		//	アニメーターを切る
		this.GetComponent<BoxCollider> ().enabled = false;	//	コライダーを切る
		playAnimation = 0;	//	アニメーションの再生フラグを折る
		boatAnimationFlag = 0;	
		boScript.breakBoat = 0;	//	ボートが壊れてるフラグを折る
	}
}
