using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScoreScript : MonoBehaviour {
	private Text score;

	// Use this for initialization
	void Start () {
		score = this.GetComponent<Text>();
		score.text = Score.scoreCount.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
