using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour {


	private float alfa;
	private float speed = 0.1f;

	private bool feadFlag = false;
	private bool whiteFeadFlag = false;

	public string nextSceneName;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//	何かしらのデス判定が来たら
		if (PlayerMove.Instance.holeDeathFlag == 1 ||
		    PlayerMove.Instance.waterDeathFlag == 1) {
			feadFlag = true;	//	フェードのフラグを立てる
		} 
		//	プレイヤーが復活したら
		else if (PlayerMove.Instance.revival) {
			feadFlag = false;	//	フェードのフラグを折る
			PlayerMove.Instance.revival = false;
		}

		if (PlayerMove.Instance.nextScene) {
			whiteFeadFlag = true;
		} else if (PlayerMove_C.Instance.nextScene) {
			whiteFeadFlag = true;
		}

		if (feadFlag) {
			GetComponent<Image> ().color = new Color (0, 0, 0, alfa);
			alfa += speed;
		} 
		else if (whiteFeadFlag) {
			GetComponent<Image> ().color = new Color (255, 255, 255, alfa);
			alfa += speed;
			if (alfa >= 1.0f) {
				StartCoroutine ("next");
			}
		}

		else {
			alfa = 0;
			GetComponent<Image> ().color = new Color (0, 0, 0, alfa);
		}
	}

	private IEnumerator next(){
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene (nextSceneName);
	}
}
