using UnityEngine;
using System.Collections;

public class WaterGimmick : MonoBehaviour {

	BoatScript boScript;

	public GameObject cloudGimmick;
	public GameObject boatGimmick;
	public GameObject moveBoat;
	public GameObject steam;

	[HideInInspector]
	public int tapRight = 0;
	[HideInInspector]
	public int tapLeft = 0;

	public AudioSource evaporationSource;
	public AudioSource flowSource;
	public AudioSource moveshipSource;

	public AudioClip evaporationSe;
	public AudioClip flowSe;
	public AudioClip moveshipSe;

	private float line = 0;
	private float gimmickLineUnder = 0;
	private float gimmickLineUp = 0;

	private Vector3 nowPosition; //= Vector3.zero;

	[HideInInspector]
	private int gimmickCount;

	private GameObject popUpCloud;
	private GameObject player;

	void Start () {
		boScript = boatGimmick.GetComponent<BoatScript> ();
		player = GameObject.FindGameObjectWithTag("Player");
		nowPosition = this.transform.position;
		gimmickLineUnder = this.transform.position.y - 2f;
		gimmickLineUp = this.transform.position.y + 2f;
	}

	void Update () {

		line = this.transform.position.y;
		if (line < gimmickLineUnder) {
			GimmickController.Instance.waterGimmickFlag = false;
			//PlayerMove.Instance.moveFlag = false;
		}
		
		if (GimmickController.Instance.waterGimmickFlag == false) {
			evaporationSource.mute = true;
			steam.gameObject.SetActive (false);
		}
		//---------------------------------------------------------------------
		//	水のギミックの発動許可が出ていて上方向にタップスライドしたら水位を下げる
		if(GimmickController.Instance.waterGimmickGo == 1 ) {
			if (GimmickController.Instance.tapPositionUP == 1) {
				if (line > gimmickLineUnder) {
					evaporationSource.mute = false;
					GimmickController.Instance.waterGimmickFlag = true;	//	水のギミッキフラグを立てる
					steam.gameObject.SetActive(true);
					this.transform.position -= new Vector3 (0, 0.01f, 0);
					if (boScript.playerOnFlag == 1)
						PlayerMove.Instance.moveFlag = false;
				}
			}
			//	ボートの挙動の制御
			//	右にスライドしたら右に移動
			if (boScript.boatMoveFlag == 1) {
				if (GimmickController.Instance.tapPositionRight == 1) {
					flowSource.mute = false;
					moveshipSource.mute = false;
					GimmickController.Instance.boatGimmickFlag = true;
					tapLeft = 0;
					tapRight = 1;
					moveBoat.transform.rotation = Quaternion.Euler (0, 0, 0);
					moveBoat.transform.position += new Vector3 (0.03f, 0, 0);
					//	ボートにプレイヤーが乗っていたら一緒に移動
					if (boScript.playerOnFlag == 1) 
						player.transform.position += new Vector3 (0.03f, 0, 0);
				} 
				//	左にスライドしたら左に移動
				else if (GimmickController.Instance.tapPositionLeft == 1) {
					flowSource.mute = false;
					moveshipSource.mute = false;
					GimmickController.Instance.boatGimmickFlag = true;
					tapRight = 0;
					tapLeft = 1;
					moveBoat.transform.rotation = Quaternion.Euler (0, 180, 0);
					moveBoat.transform.position -= new Vector3 (0.03f, 0, 0);
					//	ボートにプレイヤーが乗っていたら一緒に移動
					if(boScript.playerOnFlag == 1)
						player.transform.position -= new Vector3 (0.03f, 0, 0);
				}
			}
		}
		//---------------------------------------------------------------------

		//---------------------------------------------------------------------
		//	雲のギミックが発動したら水位を上げる
		if (GimmickController.Instance.cloudGimmickFlag) {
			if (line <= gimmickLineUp) {
					//waterGimmickFlag = 1;
					this.transform.position += new Vector3 (0, 0.01f, 0);
				}
		//---------------------------------------------------------------------
			}
		//	指が離れたら水のギミックフラグを折る
		if (GimmickController.Instance.waterGimmickGo == 0) {
			Debug.Log ("aaaaaa");
			flowSource.mute = true;
			moveshipSource.mute = true;
			evaporationSource.mute = true;
			GimmickController.Instance.waterGimmickFlag = false;
			steam.gameObject.SetActive (false);
			GimmickController.Instance.boatGimmickFlag = false;
			tapLeft = 0;
			tapRight = 0;
			boScript.boatResetCount = 0;
			}
		}		
	}
