using UnityEngine;
using System.Collections;

public class BoatScript : MonoBehaviour {

	BoatAnimation boaScript;

	Animator boatAnima;

	public int breakBoat = 0;
	public int boatMoveFlag = 1;
	[HideInInspector]
	public int playerOnFlag = 0;
	[HideInInspector]
	public int boatResetCount = 0;

	public GameObject waterGimmick;
	public GameObject cloudGimmick;
	public GameObject boatBack;


	void OnTriggerEnter(Collider boatEnd){
		//	ボートが岸に着きそうになったら
		if (boatEnd.gameObject.name == "BoatEnd") {
			boatMoveFlag = 0;
			//	どっちの岸に着いたか判定
			switch (boatEnd.gameObject.tag) {
			case "BoatEnd00":
				this.transform.position += new Vector3 (0.2f, 0, 0);
				break;
			case "BoatEnd01":
				this.transform.position += new Vector3 (-0.2f, 0, 0);
				break;
			default:
				break;
			}
		}
	}

	void OnTriggerExit(Collider boatEnd){
		if (boatEnd.gameObject.name == "BoatEnd" && boatResetCount == 0) {
			boatMoveFlag = 1;
			++boatResetCount;
		}
	}

	void OnCollisionEnter (Collision player){
		//	プレイヤーが乗っていたらフラグを立てる
		if (player.gameObject.name == "Player") {
			playerOnFlag = 1;
		}
	}

	void OnCollisionExit (Collision player){
		//	プレイヤーが降りたらフラグを折る
		if (player.gameObject.name == "Player") {
			playerOnFlag = 0;
		}
	}

	// Use this for initialization
	void Start () {
		boaScript = boatBack.GetComponent<BoatAnimation> ();
		boatAnima = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (breakBoat == 0) {
			if (boatResetCount == 0)
				boatMoveFlag = 1;
		}

		if (GimmickController.Instance.waterGimmickFlag) {
			this.transform.position -= new Vector3 (0, 0.01f, 0);
		}

		if (GimmickController.Instance.cloudGimmickFlag) {
			this.transform.position += new Vector3 (0, 0.01f, 0);
		}

		if (boaScript.playAnimation == 1) {
			boatAnima.SetBool ("BoatHeal", true);
			StartCoroutine ("Connect");
		}
			
	}
	private IEnumerator Connect()
	{
		yield return new WaitForSeconds(3.8f);
		this.GetComponent<Animator> ().enabled = false;
		boatBack.transform.SetParent (this.gameObject.transform, false);
		boatBack.transform.localPosition = Vector3.zero;
		boatBack.transform.localScale = Vector3.one;
	}
		
}
