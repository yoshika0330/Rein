using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	PlayerMove plMove;

	private GameObject player = null;
	private Vector3 offset = Vector3.zero;
	private Vector3 playerPosition;

	public Vector3 newPosition;

	public float cameraPositionZ = -16f;
	public float cameraPositionY = 8;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		plMove = player.GetComponent<PlayerMove> ();
		playerPosition = player.transform.position;
		this.transform.position = new Vector3 (playerPosition.x, playerPosition.y + cameraPositionY, cameraPositionZ);
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		newPosition = transform.position;
		newPosition.x = player.transform.position.x + offset.x;	//	左右のカメラ追従
		if(plMove.jumpFlag == 0)	//	ジャンプしてる時はカメラを追従させない
			newPosition.y = player.transform.position.y + offset.y;	//	上下のカメラ追従
		transform.position = Vector3.Lerp(transform.position,newPosition,5.0f * Time.deltaTime);
	}
}
