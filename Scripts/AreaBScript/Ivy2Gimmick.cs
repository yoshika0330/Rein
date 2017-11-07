using UnityEngine;
using System.Collections;

public class Ivy2Gimmick : MonoBehaviour {

	CloudGimmick clGimmick;

	public GameObject[] ivyGimmick;
	public GameObject cloudGimmick;
	public GameObject scaffold;
	[HideInInspector]
	public bool cameraOnFlag = false;

	public int ivyTime = 0;


	private int upCount;
	private GameObject player;

	private const string mainCamera = "MainCamera";

	// Use this for initialization
	void Start () {
		clGimmick = cloudGimmick.GetComponent<CloudGimmick> ();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void OnWillRenderObject(){
		if (Camera.current.tag == mainCamera) {
			cameraOnFlag = true;	//	カメラに写っていたらフラグを立てる
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (cameraOnFlag) {

			if (GimmickController.Instance.cloudGimmickFlag && ivyTime < 150) {
				ivyTime += 1;
			}

			if (GimmickController.Instance.ivyGimmickGo == 1 &&
				GimmickController.Instance.tapPositionDown == 1) {
				ivyTime -= 2;
			}

			//------------------------------------------------------------------
			//	植物の成長の制御
			//	一定の数値に来たらオブジェクトのアクティブをOn,Offしている
			switch (ivyTime) {
			case 1:
				ivyGimmick [1].gameObject.SetActive (false);
				scaffold.transform.position = new Vector3 (325.52f,0, 0);
				break;
			case 50:
				ivyGimmick [1].gameObject.SetActive (true);
				if(upCount == 0){
					if (PlayerMove.Instance.ivyOn) {
						player.transform.position += new Vector3 (0, 3, 0);
						scaffold.transform.position += new Vector3 (0, 5, 0);
						upCount = 1;
					}
					}
				//ivyGimmick [2].gameObject.SetActive (false);
				/*if (GimmickController.Instance.cloudGimmickFlag)
					audioSource.PlayOneShot (growSe);*/
				break;
			default:
				break;
			}
			//------------------------------------------------------------------

			if (ivyTime > 0) {
				GimmickController.Instance.ivyGimmickFlag = true;
			}

			if (ivyTime > 50) {
				ivyTime = 50;
			}

			if (ivyTime <= 0) {
				GimmickController.Instance.ivyGimmickFlag = false;
				ivyTime = 1;
				upCount = 0;
			}

			if (GimmickController.Instance.ivyGimmickGo == 0) {
				GimmickController.Instance.ivyGimmickFlag = false;
			}
		}

		cameraOnFlag = false;	//	初期化

	}
}
/*
		if (ivGimmick.ivyTime == 50 && upCount == 0) {
			if (PlayerMove.Instance.ivyOn) {
				player.transform.position += new Vector3 (0, 3, 0);
			}
			this.transform.position += new Vector3 (0, 5, 0);
			upCount = 1;
		} else if (ivGimmick.ivyTime == 1) {
			transform.position = new Vector3 (325.52f,0, 0);
		}
		else if (ivGimmick.ivyTime == 100 && upCount == 1) {
			this.transform.position += new Vector3 (0, 2, 0);
			upCount = 2;
		}
		else if (ivGimmick.ivyTime == 151 && upCount == 2) {
			this.transform.position += new Vector3 (0, 3, 0);
			upCount = 3;
		}


}
}*/
