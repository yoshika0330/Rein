using UnityEngine;
using System.Collections;

public class Arrow_C : MonoBehaviour {

	public GameObject[] arrow;

	void Start () {

	}

	void Update () {

		//	それぞれのギミックをタップした時にギッミックが動かせる矢印を表示
		if (PlayerMove_C.Instance.gimmickFlag == 1) {
			this.transform.position = Camera.main.ScreenToWorldPoint (PlayerMove_C.Instance.tapPositionFirst) + new Vector3 (0, 1, 1);
			if (GimmickManager.Instance.roboGo) {
				arrow [0].gameObject.SetActive (true);
				arrow [3].gameObject.SetActive (true);
			} else if (GimmickManager.Instance.signGimmickGo) {
				Debug.Log ("aaaaa");
				arrow [0].gameObject.SetActive (true);
				arrow [3].gameObject.SetActive (true);
			}
			else if(GimmickManager.Instance.elevatorGo){
				arrow [0].gameObject.SetActive (true);
				arrow [3].gameObject.SetActive (true);
			}
		}


		//	指を話した時に全て非表示に
		if (PlayerMove_C.Instance.tapFlag == 0 ||
			PlayerMove_C.Instance.moveFlag) {
			arrow [0].gameObject.SetActive (false);
			arrow [1].gameObject.SetActive (false);
			arrow [2].gameObject.SetActive (false);
			arrow [3].gameObject.SetActive (false);
		}
	}
}
