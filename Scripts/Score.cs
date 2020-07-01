using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	public static int scoreCount;
	public int PlayerHealthCount;

	private Text score;
	private Text PlayerHealth;

	// Use this for initialization
	void Start () {
		score = GameObject.Find ("Score").GetComponent<Text>();
		PlayerHealth = GameObject.Find("Life").GetComponent<Text>();
		reset ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addScore(int newScore){
		scoreCount += newScore;
		this.score.text = scoreCount.ToString ();
	}

	public void reset(){
		scoreCount = 0;
		this.score.text = 0.ToString ();
		this.PlayerHealthCount = 300;
		this.PlayerHealth.text = 3.ToString ();
	}

	public void DecreaseHealthBy(int sum){
		this.PlayerHealthCount -= sum;
		this.PlayerHealth.text = (PlayerHealthCount/100).ToString ();
	}

	public void IncreaseHealthBy(int sum){
		this.PlayerHealthCount += sum;
		this.PlayerHealth.text = (PlayerHealthCount/100).ToString ();
	}
}
