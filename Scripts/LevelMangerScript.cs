using UnityEngine;
using System.Collections;

public class LevelMangerScript : MonoBehaviour {
	public static int OldLevel;

	// Use this for initialization
	void Start () {

	}

	void Update(){

	}

	public void LoadNextLevel(){
		CanvasScript.IsGameStarted = false;
		int currentLevel = Application.loadedLevel;
		OldLevel = currentLevel;
		Application.LoadLevel (currentLevel + 1);
	}


	public void QuitGame(){
		Application.Quit();
	}

	public void GoTo(string levelName){
		OldLevel = Application.loadedLevel;
		Application.LoadLevel (levelName);
	}

	public void PlayAgain(){
		CanvasScript.IsGameStarted = false;
		OldLevel = Application.loadedLevel;
		Application.LoadLevel (1);
	}
}
