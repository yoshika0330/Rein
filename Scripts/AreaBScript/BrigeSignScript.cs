using UnityEngine;
using System.Collections;

public class BrigeSignScript : MonoBehaviour {

	GimmickController gimiCon;

	public GameObject gimmickController;

	Animator birgeSignAnima;

	void Start () {
		gimiCon = gimmickController.GetComponent<GimmickController> ();
		birgeSignAnima = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

			//	下にスライドされたら
			if (gimiCon.birgeSignGimmickGo == 1 && gimiCon.tapPositionDown == 1) {
				GimmickController.Instance.birigeSignGAnima = true;	//	ギミックが発動したことを知らせる
				birgeSignAnima.SetFloat ("AnimationSpeed", 1.0f);	//	再生するアニメーションのスピードを設定
				birgeSignAnima.SetBool ("BirgeSignUp", false);		//	アニメーションの逆再生用のフラグを折る
				birgeSignAnima.SetBool ("BirgeSignDown", true);		//	アニメーションの再生用のフラグを立てる
				StartCoroutine ("ssGimmickAnimaEnd");				//	ギミックが終了したことを知らせる
			}

			//	上にスライドされたら
			if (gimiCon.birgeSignGimmickGo == 1 && gimiCon.tapPositionUP == 1) {
				GimmickController.Instance.birigeSignGAnima = true;	//	ギミックが発動したことを知らせる
				birgeSignAnima.SetFloat ("AnimationSpeed", -5.0f);	//	再生するアニメーションのスピードを設定
				birgeSignAnima.SetBool ("BirgeSignDown", false);	//	アニメーションの再生用のフラグを折る
				birgeSignAnima.SetBool ("BirgeSignUp", true);	//	アニメーションの逆再生用のフラグを立てる
				StartCoroutine ("ssGimmickAnimaEnd");	//	ギミックが終了したことを知らせる
			}
		}

	private IEnumerator ssGimmickAnimaEnd()
	{
		yield return new WaitForSeconds (1.5f);
		GimmickController.Instance.birigeSignGAnima = false;
	}
		
}
