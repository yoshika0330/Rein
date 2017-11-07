using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public GameObject[] arrow;	//	ここに矢印の画像を格納

	void Start () {
	
	}

	void Update () {

		//-----------------------------------------------------------------------------------------------------------------------------
		//	それぞれのギミックをタップした時にギッミックが動かせる矢印を表示
		if (PlayerMove.Instance.gimmickFlag == 1) {
			//	タップした周辺に矢印を表示
			this.transform.position = Camera.main.ScreenToWorldPoint (PlayerMove.Instance.tapPositionFirst) + new Vector3 (0, 1, 1);
			if (GimmickController.Instance.waterGimmickGo == 1) {
				arrow [0].gameObject.SetActive (true);
				arrow [1].gameObject.SetActive (true);
				arrow [2].gameObject.SetActive (true);
			} else if (GimmickController.Instance.cloudGimmickGo == 1 ||
			           GimmickController.Instance.ivyGimmickGo == 1) {
				arrow [3].gameObject.SetActive (true);
			} else if (GimmickController.Instance.signGimmickGo == 1 ||
			           GimmickController.Instance.birgeSignGimmickGo == 1) {
				arrow [0].gameObject.SetActive (true);
				arrow [3].gameObject.SetActive (true);
			} else if (GimmickController.Instance.boatAnimationGo == 1) {
				arrow [0].gameObject.SetActive (true);
			} else if (GimmickController.Instance.bigSignGo == 1) {
				arrow [3].gameObject.SetActive (true);
			}
		}
		//-----------------------------------------------------------------------------------------------------------------------------


		//	指を話した時かプレイヤーが動き出したら全て非表示に
		if (PlayerMove.Instance.tapFlag == 0 || PlayerMove.Instance.moveFlag) {
			arrow [0].gameObject.SetActive (false);
			arrow [1].gameObject.SetActive (false);
			arrow [2].gameObject.SetActive (false);
			arrow [3].gameObject.SetActive (false);
		}
	}
}
