using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicToggleScript : MonoBehaviour {
	public Text musicText;
	public Text effectsToggleText;
	public static bool SoundEffects = true;

	// Use this for initialization
	void Start () {
		if (MusicPlayerScript.IsMusicOn) {
			this.musicText.text = "On";
		} else {
			this.musicText.text = "Off";
		}
		if(SoundEffects){
			this.effectsToggleText.text = "on";
		}else{
			this.effectsToggleText.text = "off";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToggleMusic(){
		if (MusicPlayerScript.IsMusicOn) {
			MusicPlayerScript.musicInstance.GetComponent<AudioSource>().Stop ();
			MusicPlayerScript.IsMusicOn = false;
			this.musicText.text = "Off";

		} else {
			MusicPlayerScript.musicInstance.GetComponent<AudioSource>().Play ();
			MusicPlayerScript.IsMusicOn = true;
			this.musicText.text = "On";
		}
	}

	public void ToggleEffects(){
		if (SoundEffects) {
			SoundEffects = false;
			this.effectsToggleText.text = "off";
		} else {
			SoundEffects = true;
			this.effectsToggleText.text = "on";
		}
	}
}
