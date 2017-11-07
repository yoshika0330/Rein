using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	ButtonScript nbScript;
	ButtonScript bbScript;

	public GameObject nextButton;
	public GameObject beforeButton;
	public GameObject kanbann;

	CanvasScript CS;

	public string[] scenarios00;	//	シナリオを格納する
	public string[] scenarios01;
	public string[] scenarios02;
	public string[] scenarios03;
	public string[] scenarios04;
	public string[] scenarios05;
	public string[] scenarios06;

	private string[][] popText = new string[7][];

	public int textOne = 0;

	public Text uiText;	//	uiTextへの参照を保つ

	public int currentLine = -1;

	// Use this for initialization
	void Start () {
		popText [0] = scenarios00;
		popText [1] = scenarios01;
		popText [2] = scenarios02;
		popText [3] = scenarios03;
		popText [4] = scenarios04;
		popText [5] = scenarios05;
		popText [6] = scenarios06;

		CS = GameObject.Find ("Canvas").GetComponent<CanvasScript> ();
		nbScript = nextButton.GetComponent<ButtonScript> ();
		bbScript = beforeButton.GetComponent<ButtonScript> ();

		//TextUpdate ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (CS.canvasFlag == 1 && textOne < 1) {
			TextUpdate ();
			textOne++;
		}

			//	次ボタンが押されたらテキストを進める
		if (currentLine < popText[CS.HitkanNumber].Length && nbScript.nextFlag == 1) {
			TextUpdate ();
			nbScript.nextFlag = 0;
		}
			//	前ボタンが押されたらテキストを戻す
		if (currentLine < popText[CS.HitkanNumber].Length && bbScript.beforeFlag == 1) {
				if (currentLine > 0) {
					TextBefore ();
					bbScript.beforeFlag = 0;
				}
			}
		if (currentLine == popText[CS.HitkanNumber].Length - 1) 
			CS.canvasOutFlag = 1;
	}

	//	テキストを進める処理
	public void TextUpdate() {
		currentLine++;
		uiText.text = popText[CS.HitkanNumber][currentLine];
	}
	//	テキストを戻す処理
	void TextBefore() {
		currentLine--;
		uiText.text = popText[CS.HitkanNumber] [currentLine];
	}

}
