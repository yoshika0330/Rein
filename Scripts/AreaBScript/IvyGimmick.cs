using UnityEngine;
using System.Collections;

public class IvyGimmick : MonoBehaviour {

	CloudGimmick clGimmick;

	public GameObject[] ivyGimmick;
	public GameObject cloudGimmick;
	public GameObject gimmickController;

	public bool cameraOnFlag = false;

	public int ivyTime = 0;


	public AudioSource audioSource;
	public AudioClip growSe;
	public AudioClip shrinsSe;

	private const string mainCamera = "MainCamera";

	void Start () {
		clGimmick = cloudGimmick.GetComponent<CloudGimmick> ();
	}

	void OnWillRenderObject(){
		if (Camera.current.tag == mainCamera) {
			cameraOnFlag = true;	//	カメラに写っていたらフラグを立てる
		}
	}

	void Update () {

		//	カメラに写っていたら
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
				break;
			case 50:
				ivyGimmick [1].gameObject.SetActive (true);
				ivyGimmick [2].gameObject.SetActive (false);
				if (GimmickController.Instance.cloudGimmickFlag)
					audioSource.PlayOneShot (growSe);
				if (GimmickController.Instance.ivyGimmickGo == 1 &&
					GimmickController.Instance.tapPositionDown == 1)
					audioSource.PlayOneShot (shrinsSe);
				break;
			case 100:
				ivyGimmick [2].gameObject.SetActive (true);
				ivyGimmick [1].gameObject.SetActive (false);
				ivyGimmick [3].gameObject.SetActive (false);
				if (GimmickController.Instance.cloudGimmickFlag)
					audioSource.PlayOneShot (growSe);
				if (GimmickController.Instance.ivyGimmickGo == 1 &&
					GimmickController.Instance.tapPositionDown == 1)
					audioSource.PlayOneShot (shrinsSe);
				break;
			case 150:
				ivyGimmick [3].gameObject.SetActive (true);
				ivyGimmick [2].gameObject.SetActive (false);
				if (GimmickController.Instance.cloudGimmickFlag) {
					audioSource.PlayOneShot (growSe);
				}
				GimmickController.Instance.ivyGimmickFlag = false;
				ivyTime = 151;
				break;
			default:
				break;
			}
			//------------------------------------------------------------------

			if (ivyTime > 0)
				GimmickController.Instance.ivyGimmickFlag = true;

			if (ivyTime <= 0)
				GimmickController.Instance.ivyGimmickFlag = false;

			if (GimmickController.Instance.ivyGimmickGo == 0) {
				GimmickController.Instance.ivyGimmickFlag = false;
			}
		}

		cameraOnFlag = false;	//	初期化

	}
}
