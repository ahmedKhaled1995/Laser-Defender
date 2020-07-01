using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {
	public GameObject pausePanel;
	public GameObject instructionsPanel;
	public static bool IsGamePaused;
	public static bool IsGameStarted;
	private bool inHowToPlay;

	// Use this for initialization
	void Start () {
		this.Pause();
		inHowToPlay = false;
		IsGameStarted = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(IsGameStarted && Input.GetKeyDown(KeyCode.Escape)){
			if (IsGamePaused) {
				this.Resume ();
			} else {
				this.Pause();
			}
		}
		// to hide the instructions panel when the user starts the game
		if(Input.GetMouseButtonDown(1)){
			if(!inHowToPlay && IsGamePaused){
				instructionsPanel.SetActive(false);
				IsGameStarted = true;
				this.Resume();
			}else if(inHowToPlay){
				pausePanel.SetActive (true);
				instructionsPanel.SetActive (false);
				inHowToPlay = false;
			}
		}
	}
	
	public void Resume(){
		pausePanel.SetActive (false);
		Time.timeScale = 1f;
		IsGamePaused = false;
	}
	
	void Pause(){
		if(IsGameStarted){
			pausePanel.SetActive (true);
		}
		Time.timeScale = 0f;
		IsGamePaused = true;
	}

	public void HowToPlay(){
		inHowToPlay = true;
		pausePanel.SetActive (false);
		instructionsPanel.SetActive (true);
	}
}
