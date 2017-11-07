using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class House : MonoBehaviour {

	private float alfa;
	private float speed = 0.01f;
	private bool faedFlag = false;
	private bool faedInFlag = false;

	public GameObject[] house;
	public GameObject fead;

	public AudioSource audioSource;
	public AudioClip houseSe;

	private int seCount = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
		if (GimmickManager_D.Instance.houseGo &&
		   GimmickManager_D.Instance.tapPositionUP == 1) {
			GimmickManager_D.Instance.houseAnima = true;
			faedFlag = true;
		}

		if (PlayerMove_D.Instance.nextScene) {
			faedFlag = true;
		}
			

		if (faedFlag) {
			fead.GetComponent<Image>().color = new Color (255, 255, 255, alfa);
			alfa += speed;
			/*
			if (seCount == 0) {
				seCount++;
				audioSource.PlayOneShot (houseSe);
			}
			*/
		}
		if (alfa >= 1.0f) {
			if (PlayerMove_D.Instance.nextScene) {
				SceneManager.LoadScene ("EndSene");
			}
			faedFlag = false;
			house [0].gameObject.SetActive (false);
			house [1].gameObject.SetActive (true);
			faedInFlag = true;
		}
		if (faedInFlag) {
			fead.GetComponent<Image>().color = new Color (255, 255, 255, alfa);
			alfa -= speed;
			if (alfa <= 0) {
				faedInFlag = false;
				GimmickManager_D.Instance.houseAnima = false;
			}
		}
	}
}
