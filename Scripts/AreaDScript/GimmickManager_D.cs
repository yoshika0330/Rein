using UnityEngine;
using System.Collections;

public class GimmickManager_D : MonoBehaviour {

	#region Singlton
	public static GimmickManager_D Instance{ get; private set; }
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
	public bool houseGo = false;
	[HideInInspector]
	public bool houseAnima = false;
	//---------------------------------


	public bool signGAnima = false;

	void Start () {
	}

	// Update is called once per frame
	void Update () {
		//------------------------------------------------------------------
		//	ギミックをタップしていてそぞれが何かを比較してそれぞれのギミックの発動許可を出す

		if(PlayerMove_D.Instance.gimmickFlag == 1){
			switch (PlayerMove_D.Instance.obj.name) {
			case "Loghouse_Broken":
				houseGo = true;
				break;
			default:
				break;
			}

		}

		//	ギミックから指が離れたらそれぞれのギミックの発動許可を解除する

		if (PlayerMove_D.Instance.gimmickFlag == 0) {
			houseGo = false;
		}
		//------------------------------------------------------------------
		//	左右にフラグがたってない時に
		if (tapPositionRight == 0 && tapPositionLeft == 0) {
			//	タップした所から上に行ったら
			if (PlayerMove_D.Instance.tapPositionFirst.y + 20 < 
				PlayerMove_D.Instance.tapPosition.y)
				tapPositionUP = 1;	//	上フラグを立てる
			//	タップした所から下に行ったら
			else if (PlayerMove_D.Instance.tapPositionFirst.y - 20 >
				PlayerMove_D.Instance.tapPosition.y)
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
			if (PlayerMove_D.Instance.tapPositionFirst.x + 20 < 
				PlayerMove_D.Instance.tapPosition.x)
				tapPositionRight = 1;	//	右フラグを立てる
			//	タップした所から左に行ったら
			else if (PlayerMove_D.Instance.tapPositionFirst.x - 20 >
				PlayerMove_D.Instance.tapPosition.x)
				tapPositionLeft = 1;		//	左フラグを立てる
			//	真ん中に戻ったら
			else {
				//	左右のフラグを折る
				tapPositionRight = 0;
				tapPositionLeft = 0;
			}
		}

		//	タップが離れたら全ての位置フラグを折る
		if(PlayerMove_D.Instance.tapFlag == 0){
			tapPositionUP = 0;
			tapPositionDown = 0;
			tapPositionRight = 0;
			tapPositionLeft = 0;
		}


	}


}

