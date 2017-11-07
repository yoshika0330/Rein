using UnityEngine;
using System.Collections;

public class CloudGimmick : MonoBehaviour {

	GimmickController gController;
	WaterGimmick wGimmick;

	public GameObject gimmickControl;
	public GameObject waterGimmick;
	public GameObject rainEffect;
	public GameObject cloudGimmick;

	public AudioSource audioSource;

	public AudioClip rainSe;

	[HideInInspector]
	public float alpha = 0;

	public GameObject nullObj;

	private SpriteRenderer spRenderer;

	private GameObject player;
	private GameObject popUpRainEffect;
	private GameObject popUpCloud;

	private int gimmickCount = 0;

	private bool destroyRainGimmickFlag = false;

	void Start () {
		gController = gimmickControl.GetComponent<GimmickController> ();
		wGimmick = waterGimmick.GetComponent<WaterGimmick>();
		player = GameObject.FindGameObjectWithTag("Player");	//	プレイヤーを取得
		this.transform.position = player.transform.position + new Vector3 (4, 0, 0);	//	プレイヤーの頭上らへんに来るようにposition設定
		spRenderer = GetComponent<SpriteRenderer>();
		spRenderer.color = new Color (1, 1, 1, 0);		//	雲のalpha値など
		popUpRainEffect = Instantiate (nullObj);	//	用意したnullobjectを代入
	}
		
	void Update () {
		//	雲の位置をプレイヤーの真上に更に追従するように
		this.transform.position = player.transform.position + new Vector3 (4, 4, 0);
		var color = spRenderer.color;
		color.a = alpha;
		spRenderer.color = color;

		//	もし雲が表示されてなかったらギミックを発動させないようにする
		if (alpha == 0)
			gController.cloudGimmickGo = 0;

		//	水のギミック発動フラグが立っていたら
		if (GimmickController.Instance.waterGimmickFlag)
			alpha += 0.005f;	//	雲を徐々に出す
		
		//	雲のギミック発動許可が出ていてタップスライドで下に行ったら
		if (gController.cloudGimmickGo == 1 ) {
			if (gController.tapPositionDown == 1) {
				audioSource.mute = false;
				if (gimmickCount == 0) {
					gimmickCount++;
					popUpRainEffect = Instantiate (rainEffect);	//	雨のフェクトを表示
					popUpRainEffect.transform.position = player.transform.position;
				}
				GimmickController.Instance.cloudGimmickFlag = true;	//	雲のギミックフラグを立てる
				alpha -= 0.005f;		//	雲を徐々に消す
			} else {
				GimmickController.Instance.cloudGimmickFlag = false;	
				destroyRainGimmickFlag = true;	//	雨のギミック停止フラグを立てる
			}
		}

		//	もし雲のギミック発動許可がなかったら
		if (gController.cloudGimmickGo == 0) {
			GimmickController.Instance.cloudGimmickFlag = false;			//	雲のギミックフラグを折って
			destroyRainGimmickFlag = true;	//	雨のギミック停止フラグを立てる
		}

		//----------------------------------
		//	alpha値の調整
		if (alpha >= 1)
			alpha = 1;
		else if (alpha <= 0) {
			alpha = 0;
			destroyRainGimmickFlag = true;
		}
		//----------------------------------

		if (destroyRainGimmickFlag) {
			Destroy (popUpRainEffect.gameObject);	//	まず雨を消して
			gimmickCount = 0;						//	ギミックカウントを初期化
			audioSource.mute = true;				//	雨の音をミュートにして
			destroyRainGimmickFlag = false;			//	雨のギミック停止フラグを折る
		}
		
				
	}
}
