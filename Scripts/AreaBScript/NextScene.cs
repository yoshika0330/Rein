using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {

	void OnTriggerEnter (Collider collision) {
		if (collision.gameObject.tag == "Player") {
			SceneManager.LoadScene ("LaboScene");
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
