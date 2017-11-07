using UnityEngine;
using System.Collections;

public class SignAnimationScript : MonoBehaviour {

	SignScript siScript;

	Animator signAnima;

	public GameObject sign;

	public bool cameraOnFlag = false;

	public AudioSource audioSource;
	public AudioClip sigmbreakSe;

	private const string mainCamera = "MainCamera";

	// Use this for initialization
	void Start () {
		siScript = sign.GetComponent<SignScript> ();
		signAnima = GetComponent<Animator>();
	}
		
	void OnWillRenderObject(){
		if (Camera.current.tag == mainCamera) {
			cameraOnFlag = true;	//	カメラに写っていたらフラグを立てる
		}
	}

	// Update is called once per frame
	void Update () {

		//	カメラに写っていたら
		if (cameraOnFlag) {
			//	下にスライドされたら
			if (siScript.signDownFlag == 1) {
				GimmickController.Instance.signGAnima = true;
				signAnima.SetFloat ("AnimationSpeed", 1.0f);	//	アニメーションの再生スピード
				signAnima.SetBool ("backAnimation", false);		//	アニメーションの逆再生用のフラグを折る
				signAnima.SetBool ("playAnimation", true);		//	アニメーションの再生用のフラグを立てる
				siScript.textFlag = 0;	//	キャンパス表示用のフラグを立てる
				siScript.signDownFlag = 0;	//	看板が壊れていることを知らせる
				StartCoroutine("gimmickEnd");
			}
			//	上にスライドされたら
			if (siScript.signUpFlag == 1) {
				GimmickController.Instance.signGAnima = true;
				signAnima.SetFloat ("AnimationSpeed", 1.0f);	//	アニメーションの再生スピード
				signAnima.SetBool ("playAnimation", false);		//	アニメーションの再生用のフラグを折る
				signAnima.SetBool ("backAnimation", true);		//	アニメーションの逆再生用のフラグを立てる
				siScript.textFlag = 1;	//	キャンパス表示用のフラグを折る
				siScript.signUpFlag = 0;	//	看板が治ってることを知らせる
				StartCoroutine("gimmickEnd");
			}
		}

		cameraOnFlag = false;	//	初期化
			
	}
	private IEnumerator gimmickEnd()
	{
		yield return new WaitForSeconds (1.5f);
		GimmickController.Instance.signGAnima = false;
	}
}
