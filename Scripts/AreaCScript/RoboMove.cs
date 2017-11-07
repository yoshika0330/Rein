using UnityEngine;
using System.Collections;

public class RoboMove : MonoBehaviour {

	public GameObject animePlayer;

	public bool isBreak = true;
	public bool moveFlag = false;
	public bool isPlayerBack = false;


	private bool isLeft = true;
	private bool isRight = false;

	private int rotateCount = 0;

	private Script_SpriteStudio_Root ScriptRoot;

	enum AnimationType
	{
		break_ = 0,
		break_idle,
		idle,
		idle_,
		idle_Q,
		respair
	}
	private AnimationType motion = AnimationType.break_idle;

	void Start () {
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.name == "RoboReturn") {
			if(isLeft)
				this.transform.rotation = Quaternion.Euler (0, 90, 0);
			else if(isRight)
				this.transform.rotation = Quaternion.Euler (0, -90, 0);
		}
		
	}
	void OnTriggerEnter (Collider collider)
	{
		if (collider.gameObject.name == "RoboReturn") {
			if(isLeft)
				this.transform.rotation = Quaternion.Euler (0, 90, 0);
			else if(isRight)
				this.transform.rotation = Quaternion.Euler (0, -90, 0);
		}

	}

	void Update () {
		if(null == ScriptRoot)
		{
			//初期化.
			ScriptRoot = Library_SpriteStudio.Utility.Parts.RootGetChild(animePlayer);
			//アニメーション終了コールバックを設定.
			ScriptRoot.FunctionPlayEnd = AnimEnd;
			motion = AnimationType.break_idle;
			//アニメーション再生
			ScriptRoot.AnimationPlay((int)AnimationType.break_idle, 0, 0, 1.0f);	
		}

		if (isBreak) {
			moveFlag = false;
			if (GimmickManager.Instance.roboGo &&
			    GimmickManager.Instance.tapPositionUP == 1) {
				GimmickManager.Instance.roboAnima = true;
				motion = AnimationType.respair;
				ScriptRoot.AnimationPlay ((int)AnimationType.respair, 1, 0, 1.0f);
			}
		} 
		else if (isBreak == false) {
			
			if (GimmickManager.Instance.roboGo == false)
				moveFlag = true;

			if (GimmickManager.Instance.roboGo &&
				GimmickManager.Instance.tapPositionDown == 1) {
				GimmickManager.Instance.roboAnima = true;
				moveFlag = false;
				motion = AnimationType.break_;
				ScriptRoot.AnimationPlay ((int)AnimationType.break_, 1, 0, 1.0f);
			}
		}

		if (moveFlag) {
			if (isPlayerBack == false) {
				this.transform.position += transform.TransformDirection (Vector3.forward) * 0.1f;
			}
			if (PlayerMove_C.Instance.playerDirectionRight) {
				if (this.transform.position.x <= (PlayerMove_C.Instance.transform.position.x - 1.0f) &&
				    this.transform.position.x >= (PlayerMove_C.Instance.transform.position.x - 2.0f)) {
					isPlayerBack = true;
					this.transform.rotation = Quaternion.Euler (0, 90, 0);
					isLeft = false;
					isRight = true;
				} else
					isPlayerBack = false;

			}
			else if (PlayerMove_C.Instance.playerDirectionLeft) {
				if (this.transform.position.x >= (PlayerMove_C.Instance.transform.position.x + 1.0f) &&
				    this.transform.position.x <= (PlayerMove_C.Instance.transform.position.x + 2.0f)) {
					isPlayerBack = true;
					this.transform.rotation = Quaternion.Euler (0, -90, 0);
					isRight = false;
					isLeft = true;
				} else
					isPlayerBack = false;
			}
			if (GimmickManager.Instance.roboGo)
				moveFlag = false;
	}
	
	}

	public bool AnimEnd(Script_SpriteStudio_Root InstanceRoot,GameObject ObjectControl)
	{
		switch (motion) {
		case AnimationType.respair:
			GimmickManager.Instance.roboAnima = false;
			if(this.transform.position.x < PlayerMove_C.Instance.transform.position.x)
				this.transform.rotation = Quaternion.Euler (0, 90, 0);
			else if(this.transform.position.x > PlayerMove_C.Instance.transform.position.x)
				this.transform.rotation = Quaternion.Euler (0, -90, 0);
			motion = AnimationType.idle;
			ScriptRoot.AnimationPlay ((int)AnimationType.idle, 0, 0, 1.0f);
			isBreak = false;
			moveFlag = true;
			break;
		case AnimationType.break_:
			GimmickManager.Instance.roboAnima = false;
			motion = AnimationType.break_idle;
			ScriptRoot.AnimationPlay ((int)AnimationType.break_idle, 0, 0, 1.0f);
			isBreak = true;
			break;
		default:
			break;
		}
		return true;
	}
		
}
