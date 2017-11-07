using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMove_C : MonoBehaviour {

	CameraMove cMove;

	#region Singlton
	public static PlayerMove_C Instance{ get; private set; }
	void Awake()
	{
		if (Instance != null) 
		{
			enabled = false;
			DestroyImmediate (this);
			return;
		}
		Instance = this;
	}
	#endregion

	private Script_SpriteStudio_Root ScriptRoot;

	private float width;
	private float height;
	private float middle;
	private float idleCount;

	private bool installationFlag = false; // isGrounded
	private bool gimmickAnimationFlag = false;
	public bool playerDirectionRight = true; // doRight
	public bool playerDirectionLeft = false; // doLeft
	private bool standFlag = false;

	private int jumpCount = 0;
	private int rotateCountRight = 0;
	private int rotateCountLeft = 1;
	private int tapCount = 0;
	private int seCount = 0;

	private Vector3 worldPos;
	private Vector3 updateSavePoint;

	private GameObject popUpTapEffect;

	[HideInInspector]
	public Vector3 tapPosition;
	[HideInInspector]
	public Vector3 tapPositionFirst;

	public float moveSpeed = 0.1f;

	public int jumpFront = 5;
	public int jumpUp = 20;
	[HideInInspector]
	public int jumpFlag = 0;
	[HideInInspector]
	public int gimmickFlag = 0;
	[HideInInspector]
	public int tapFlag = 0;

	public GameObject obj;
	public GameObject nullObj;
	public GameObject animePlayer;
	public GameObject tapEffect;

	public AudioSource audioSource;

	public AudioClip jumpSe;
	public AudioClip gimmickSe;
	public AudioClip tapSe;

	[HideInInspector]
	public bool moveFlag = true;

	public bool nextScene = false;

	enum AnimationType
	{
		idle = 13,
		idle_Q_L,
		idle_Q_R,
		jump_All,
		jump_1,
		jump_2,
		jump_3,
		run,
		up_1,
		up_2,
		up_3,
		up_All
	}

	private AnimationType motion = AnimationType.run;		//表示モーション

	void Start () {
		width = Screen.width;	//	画面の横幅取得
		height = Screen.height;	//	画面の縦幅取得
		middle = width / 2;		//	画面の真ん中取得

	}

	void OnTriggerEnter(Collider nextCol){
		if (nextCol.gameObject.name == "NextScene") {
			nextScene = true;
			moveFlag = false;
		}
	}

	void OnCollisionEnter (Collision collision) 
	{
		switch (collision.gameObject.tag) {
		case "Ground":
		case "Gimmick":
		case "Button":
		case "Robo":
			standFlag = true;
			break;
		default:
			break;
		}

		//	何かに足をつけていたら
		if (standFlag){
			installationFlag = true;	//	設置フラグを立てる
			moveFlag = true;			//	ムーブフラグを立てる
			jumpCount = 0;				//	ジャンプカウントのリセット
		}

	}
		

	void CheckGroundeed()
	{
		//----------------------------------------------------------------------
		//	プレイヤーが地面に付いているとき。　
		if (installationFlag) {
			// 画面をタップした時
			if (Input.GetMouseButtonDown (0) ) {

				tapFlag = 1;		//	タップされたらフラグを立てる
				tapPositionFirst = Input.mousePosition;

				worldPos = Camera.main.ScreenToWorldPoint(tapPositionFirst) + new Vector3 (0,0,1);

				//-------------------------------------------------------------
				//	Raycastなどの処理
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit = new RaycastHit();
				if (Physics.Raycast (ray, out hit)) {
					obj = hit.collider.gameObject;
					Debug.Log (obj);
				}
				//-------------------------------------------------------------

				if (obj.tag == "Gimmick" || obj.tag == "Robo"){
					moveFlag = false;	//	触れたものがギミックなら動きを止める
					gimmickFlag = 1;
				}

				tapEffect.transform.position = worldPos;

				if (tapCount == 0) {
					audioSource.PlayOneShot (tapSe);
					tapCount++;
					popUpTapEffect = Instantiate (tapEffect);	//	 タップエフェクトを表示
				}
			}
		}
	}

	void Update () {

		if(null == ScriptRoot)
		{
			//初期化.
			ScriptRoot = Library_SpriteStudio.Utility.Parts.RootGetChild(animePlayer);
			//アニメーション終了コールバックを設定.
			ScriptRoot.FunctionPlayEnd = AnimEnd;
			motion = AnimationType.idle;
			//アニメーション再生
			ScriptRoot.AnimationPlay((int)AnimationType.idle, 0, 0, 1.0f);	
		}
			

		CheckGroundeed ();

		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		//	画面をタップしてる間
		if (Input.GetMouseButton (0)) {
			tapPosition = Input.mousePosition;	//	タップいている場所を代入
			worldPos = Camera.main.ScreenToWorldPoint (tapPosition) + new Vector3(0,0,1);	//	ワールド座標に変化
			popUpTapEffect.transform.position = worldPos;	//	タップフェクトをタップしているところに常に表示

		}
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		//	画面から指を話したら
		if (Input.GetMouseButtonUp (0)) {
			tapFlag = 0;		//	タップが離れたらフラグを折る
			DestroyTapEffects ();	//	タップエフェクトを消去	
			tapCount = 0;

			//	ギミックをタップするのを離れたら動けるようにする
			if (obj.tag == "Gimmick" || obj.tag == "Robo"){
				gimmickFlag = 0;
				moveFlag = true;	
			}

			obj = nullObj;

			//	指を話した時にアニメーションがrunならアニメーションをidleに変える
			if (motion == AnimationType.run) {
				motion = AnimationType.idle;
				ScriptRoot.AnimationPlay ((int)AnimationType.idle, 0, 0, 1.0f);	
			}
		}

		if (motion == AnimationType.idle)
			idleCount += 0.01f;
		else
			idleCount = 0;

		if (motion == AnimationType.idle && idleCount > 3.0f) {
			if (playerDirectionLeft) {
				motion = AnimationType.idle_Q_L;
				ScriptRoot.AnimationPlay ((int)AnimationType.idle_Q_L, 1, 0, 1.0f);
			}
			else if (playerDirectionRight) {
				motion = AnimationType.idle_Q_R;
				ScriptRoot.AnimationPlay ((int)AnimationType.idle_Q_R, 1, 0, 1.0f);
			}
		}
		//----------------------------------------------------------------------	
		if(tapFlag == 1){

			if (moveFlag) {

				//	アニメーションをrunに切り替え
				if(motion != AnimationType.run){
					motion = AnimationType.run;
					ScriptRoot.AnimationPlay((int)AnimationType.run, 0, 0, 1f);
				}

				//-----------------------------------------------------------------------------------------------
				//	タップした時の移動処理
				//	画面中央より左を押した時
				if (0 < tapPosition.x && tapPosition.x < middle) {
					rotateCountLeft = 0;			//	左の回転カウントを初期化
					playerDirectionRight = false;	//	右向きフラグを折る
					playerDirectionLeft = true;		//	左向きフラグを立てる
					//	向いてる方向に動く
					transform.position += transform.TransformDirection (Vector3.forward) * moveSpeed;
				} 
				//	画面中央より右を押した時
				else if (middle < tapPosition.x) {
					rotateCountRight = 0;				//	右の回転カウントを初期化
					playerDirectionLeft = false;		//	左向きフラグを折る
					playerDirectionRight = true;		//	右向きフラグを立てる
					//	向いてる方向に動く
					transform.position += transform.TransformDirection (Vector3.forward) * moveSpeed;
				}
				// 中央部分かつプレイヤーを押した時
				if (installationFlag && obj.tag == "Player")
					jumpFlag = 1;		//	ジャンプフラグを立て
				//-----------------------------------------------------------------------------------------------

			}

			//--------------------------------------------------------------------------------------
			//	プレイヤーのジャンプ処理
			if (jumpFlag == 1) {
				motion = AnimationType.jump_1;
				ScriptRoot.AnimationPlay((int)AnimationType.jump_1, 1, 0, 1f);
				moveFlag = false;	//	ムーブフラグを折る
				installationFlag = false;	//	地面から離れたら設置フラグを折る

			}
			//--------------------------------------------------------------------------------------

		}

		//--------------------------------------------------------------------
		//	プレイヤーの方向転換処理
		if (playerDirectionRight) {
			if (rotateCountLeft < 1) {
				transform.Rotate (new Vector3 (0, 180, 0));
				rotateCountLeft++;		//	回った後にカウントアップ
			}
		} 
		else if (playerDirectionLeft) {
			if (rotateCountRight < 1) {
				transform.Rotate (new Vector3 (0, 180, 0));
				rotateCountRight++;		//	回った後にカウントアップ
			}
		}
		//--------------------------------------------------------------------

		//-------------------------------------------------------------------------
		//	それぞれのギミックを発動させたらそれ用のアニメーションを再生させるフラグを立てる
		if (GimmickManager.Instance.roboAnima)
			gimmickAnimationFlag = true;
		else if (GimmickManager.Instance.signGAnima)
			gimmickAnimationFlag = true;
		else if (GimmickManager.Instance.elevatorAnima)
			gimmickAnimationFlag = true;
		else
			gimmickAnimationFlag = false;

		//	ギミッキを発動させた時のアニメーション切り替え
		if (gimmickAnimationFlag) {
			switch (motion) {
			case AnimationType.idle:
			case AnimationType.idle_Q_R:
			case AnimationType.idle_Q_L:
			case AnimationType.run:
				motion = AnimationType.up_1;
				ScriptRoot.AnimationPlay ((int)AnimationType.up_1, 1, 0, 1f);
				if (seCount == 0) {
					seCount++;
					audioSource.mute = false;
					audioSource.PlayOneShot(gimmickSe);
				}
				break;
			default:
				break;
			}
		}

		if (gimmickAnimationFlag)
			moveFlag = false;

		if (gimmickAnimationFlag == false) {
			switch (motion) {
			case AnimationType.up_2:
				moveFlag = true;
				motion = AnimationType.up_3;
				ScriptRoot.AnimationPlay((int)AnimationType.up_3, 1,0,1.0f);
				break;
			default:
				break;
			}
		}
	}

	public bool AnimEnd(Script_SpriteStudio_Root InstanceRoot,GameObject ObjectControl)
	{
		//アニメーション制御
		switch(motion)
		{
		case AnimationType.run:
			motion = AnimationType.idle;
			ScriptRoot.AnimationPlay ((int)AnimationType.idle, 0, 0, 0.5f);	
			break;
			//	--------------------------------------------------------------------------------------
			//	ジャンプアニメーションの制御
		case AnimationType.jump_1:
			jumpCount++;
			motion = AnimationType.jump_2;
			ScriptRoot.AnimationPlay((int)AnimationType.jump_2, 1, 0, 0.6f);
			if (jumpCount == 1) {
				if (playerDirectionRight) 
					//	もし地面に設置していたらプレイヤーの向きにジャンプする
					transform.GetComponent<Rigidbody> ().AddForce (jumpFront, jumpUp, 0);
				else 
					//	もし地面に設置していたらプレイヤーの向きにジャンプする
					transform.GetComponent<Rigidbody> ().AddForce (jumpFront * -1, jumpUp, 0);
				standFlag = false;
			}
			//----------------------------------------
			//ジャンプのSEを再生する
			if (seCount == 0) {
				seCount++;
				audioSource.mute = false;
				audioSource.PlayOneShot(jumpSe);
			}
			//----------------------------------------
			break;
		case AnimationType.jump_2:
			jumpFlag = 0;
			jumpCount = 0;
			seCount = 0;
			motion = AnimationType.jump_3;
			ScriptRoot.AnimationPlay ((int)AnimationType.jump_3, 1, 0, 1.0f);
			break;
		case AnimationType.jump_3:
			motion = AnimationType.idle;
			ScriptRoot.AnimationPlay ((int)AnimationType.idle, 0, 0, 1.0f);
			break;
			//	--------------------------------------------------------------------------------------

			//	--------------------------------------------------------------------------------------
			//	ギミックを発動させた時のアニメーション
		case AnimationType.up_1:
			seCount = 0;
			motion = AnimationType.up_2;
			ScriptRoot.AnimationPlay ((int)AnimationType.up_2, 0, 0, 1.0f);
			break;
		case AnimationType.up_2:
			motion = AnimationType.up_3;
			ScriptRoot.AnimationPlay ((int)AnimationType.up_3, 1, 0, 1.0f);
			break;
		case AnimationType.up_3:
			motion = AnimationType.idle;
			ScriptRoot.AnimationPlay ((int)AnimationType.idle, 0, 0, 1.0f);
			break;
			//	--------------------------------------------------------------------------------------

			//	--------------------------------------------------------------------------------------
			//	アイドルのアニメーション
		case AnimationType.idle_Q_L:
			motion = AnimationType.idle;
			ScriptRoot.AnimationPlay ((int)AnimationType.idle, 0, 0, 1.0f);
			idleCount = 0;
			break;
		case AnimationType.idle_Q_R:
			motion = AnimationType.idle;
			ScriptRoot.AnimationPlay ((int)AnimationType.idle, 0, 0, 1.0f);
			idleCount = 0;
			break;
			//	--------------------------------------------------------------------------------------
		default:
			break;
		}
		return true;
	}

	//	タップエフェクト削除
	void DestroyTapEffects(){
		Destroy (popUpTapEffect.gameObject);
	}
}