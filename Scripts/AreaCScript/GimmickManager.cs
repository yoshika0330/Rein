using UnityEngine;
using System.Collections;

public class GimmickManager : MonoBehaviour {

	#region Singlton
	public static GimmickManager Instance{ get; private set; }
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

	public GameObject player;

	//---------------------------------
	//	タップしてどっちにスライドさせてるかのフラグ
	[HideInInspector]
	public int tapPositionUP = 0;
	[HideInInspector]
	public int tapPositionDown = 0;
	[HideInInspector]
	public int tapPositionRight = 0;
	[HideInInspector]
	public int tapPositionLeft = 0;
	//---------------------------------

	//---------------------------------
	//	それぞれのギミックの発動許可フラグ
	[HideInInspector]
	public bool signGimmickGo = false;
	[HideInInspector]
	public bool elevatorGo = false;
	[HideInInspector]
	public bool roboGo = false;
	//---------------------------------


	public bool signGAnima = false;
	public bool elevatorAnima = false;
	public bool roboAnima = false;

	void Start () {
	}

	// Update is called once per frame
	void Update () {
		//------------------------------------------------------------------
		//	ギミックをタップしていてそぞれが何かを比較してそれぞれのギミックの発動許可を出す

		if(PlayerMove_C.Instance.gimmickFlag == 1){
			switch (PlayerMove_C.Instance.obj.name) {
			case "Elevator":
				elevatorGo = true;
				break;
			case "Robo":
				roboGo = true;
				break;
			case "S_Kanban_Support":
				signGimmickGo = true;
				break;
			default:
				break;
			}

		}

		//	ギミックから指が離れたらそれぞれのギミックの発動許可を解除する

		if (PlayerMove_C.Instance.gimmickFlag == 0) {
			elevatorGo = false;
			roboGo = false;
			signGimmickGo = false;
		}
		//------------------------------------------------------------------
		//	左右にフラグがたってない時に
		if (tapPositionRight == 0 && tapPositionLeft == 0) {
			//	タップした所から上に行ったら
			if (PlayerMove_C.Instance.tapPositionFirst.y + 20 < 
				PlayerMove_C.Instance.tapPosition.y)
				tapPositionUP = 1;	//	上フラグを立てる
			//	タップした所から下に行ったら
			else if (PlayerMove_C.Instance.tapPositionFirst.y - 20 >
				PlayerMove_C.Instance.tapPosition.y)
				tapPositionDown = 1;	//	下フラグを立てる
			//	真ん中に戻ったら
			else {
				//	上下のフラグを折る
				tapPositionUP = 0;
				tapPositionDown = 0;
			}
		}

		//	上下のフラグがたってない時に
		if (tapPositionUP == 0 && tapPositionDown == 0) {
			//	タップした所から右に行ったら
			if (PlayerMove_C.Instance.tapPositionFirst.x + 20 < 
				PlayerMove_C.Instance.tapPosition.x)
				tapPositionRight = 1;	//	右フラグを立てる
			//	タップした所から左に行ったら
			else if (PlayerMove_C.Instance.tapPositionFirst.x - 20 >
				PlayerMove_C.Instance.tapPosition.x)
				tapPositionLeft = 1;		//	左フラグを立てる
			//	真ん中に戻ったら
			else {
				//	左右のフラグを折る
				tapPositionRight = 0;
				tapPositionLeft = 0;
			}
		}

		//	タップが離れたら全ての位置フラグを折る
		if(PlayerMove_C.Instance.tapFlag == 0){
			tapPositionUP = 0;
			tapPositionDown = 0;
			tapPositionRight = 0;
			tapPositionLeft = 0;
		}


	}


}

