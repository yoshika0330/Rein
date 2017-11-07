using UnityEngine;
using System.Collections;

public class GimmickController : MonoBehaviour {
	// Use this for initialization
	PlayerMove plMove;

	#region Singlton
	public static GimmickController Instance{ get; private set; }
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
	public int waterGimmickGo = 0;
	[HideInInspector]
	public int cloudGimmickGo = 0;
	[HideInInspector]
	public int signGimmickGo = 0;
	[HideInInspector]
	public int birgeSignGimmickGo = 0;
	[HideInInspector]
	public int ivyGimmickGo = 0;
	[HideInInspector]
	public int boatAnimationGo = 0;
	[HideInInspector]
	public int bigSignGo = 0;
	//---------------------------------

	public bool waterGimmickFlag = false;
	public bool cloudGimmickFlag = false;
	public bool boatGimmickFlag = false;
	public bool ivyGimmickFlag = false;
	public bool birigeSignGAnima = false;
	public bool bigSignGAnima = false;
	public bool signGAnima = false;

	void Start () {
		plMove = player.GetComponent<PlayerMove> ();
	}
	
	// Update is called once per frame
	void Update () {
		//------------------------------------------------------------------
		//	ギミックをタップしていてそぞれが何かを比較してそれぞれのギミックの発動許可を出す
		if (plMove.gimmickFlag == 1) {
			switch (plMove.obj.name) {
			case "Water":
				waterGimmickGo = 1;
				break;
			case "Cloud":
				cloudGimmickGo = 1;
				break;
			case "S_Kanban_Support":
				signGimmickGo = 1;
				break;
			case "M_Kanban_display":
				birgeSignGimmickGo = 1;
				break;
			case "Ivy":
				ivyGimmickGo = 1;
				break;
			case "Boat_Back":
				boatAnimationGo = 1;
				break;
			case "L_Kanban_display_L":
			case "L_Kanban_display_R":
				bigSignGo = 1;
				break;
			default:
				break;
			}
		}

		//	ギミックから指が離れたらそれぞれのギミックの発動許可を解除する
		if (plMove.gimmickFlag == 0) {
			waterGimmickGo = 0;
			cloudGimmickGo = 0;
			signGimmickGo = 0;
			birgeSignGimmickGo = 0;
			ivyGimmickGo = 0;
			boatAnimationGo = 0;
			bigSignGo = 0;
		} 

		//------------------------------------------------------------------
			//	左右にフラグがたってない時に
			if (tapPositionRight == 0 && tapPositionLeft == 0) {
				//	タップした所から上に行ったら
				if (plMove.tapPositionFirst.y + 20 < plMove.tapPosition.y)
					tapPositionUP = 1;	//	上フラグを立てる
				//	タップした所から下に行ったら
				else if (plMove.tapPositionFirst.y - 20 > plMove.tapPosition.y)
						tapPositionDown = 1;	//	下フラグを立てる
				//	真ん中に戻ったら
				else {
					waterGimmickFlag = false;
					//	上下のフラグを折る
					tapPositionUP = 0;
					tapPositionDown = 0;
				}
			}

			//	上下のフラグがたってない時に
			 if (tapPositionUP == 0 && tapPositionDown == 0) {
				//	タップした所から右に行ったら
				if (plMove.tapPositionFirst.x + 20 < plMove.tapPosition.x)
					tapPositionRight = 1;	//	右フラグを立てる
				//	タップした所から左に行ったら
				else if (plMove.tapPositionFirst.x - 20 > plMove.tapPosition.x)
					tapPositionLeft = 1;		//	左フラグを立てる
				//	真ん中に戻ったら
				else {
					//	左右のフラグを折る
					tapPositionRight = 0;
					tapPositionLeft = 0;
				}
			}

		//	タップが離れたら全ての位置フラグを折る
		if(plMove.tapFlag == 0){
				tapPositionUP = 0;
				tapPositionDown = 0;
				tapPositionRight = 0;
				tapPositionLeft = 0;
			}

			
		}
		

	}
