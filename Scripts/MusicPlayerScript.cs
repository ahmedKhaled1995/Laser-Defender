using UnityEngine;
using System.Collections;

public class MusicPlayerScript : MonoBehaviour {
	public static MusicPlayerScript musicInstance = null;
	public static bool IsMusicOn = false;
	private AudioSource music;
	public AudioClip startAndOptions;
	public AudioClip game;
	public AudioClip end;

	void Awake(){
		if(musicInstance){
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		music = this.GetComponent<AudioSource>();
		if(musicInstance == null){   // first time music is initialized
			IsMusicOn = true;
			musicInstance = this;
			music.clip = startAndOptions;
			music.loop = true;
			music.Play ();
		}
		DontDestroyOnLoad (gameObject);
	} 
		
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("Music player updates: " + GetInstanceID());
	}

	void OnLevelWasLoaded(int level){
		if(IsMusicOn){
			// 0 is the main menu
			// 2 is the options menu
			// 3 is how to play
			// they both share the music
			if(level == 0 && (LevelMangerScript.OldLevel != 2 && LevelMangerScript.OldLevel != 3)){
				if(music){
					music.Stop ();
					music.clip = startAndOptions;
					music.loop = true;
					music.Play();
				}
			}else if(level == 2 && (LevelMangerScript.OldLevel != 0 && LevelMangerScript.OldLevel != 3)){
				music.Stop();
				music.clip = startAndOptions;
				music.loop = true;
				music.Play();
			}else if(level == 3 && (LevelMangerScript.OldLevel != 0 && LevelMangerScript.OldLevel != 2)){
				music.Stop();
				music.clip = startAndOptions;
				music.loop = true;
				music.Play();
			}else if(level == 1){
				music.Stop();
				music.clip = game;
				music.loop = true;
				music.Play();
			}else if(level == 4){
				music.Stop();
				music.clip = end;
				music.loop = true;
				music.Play();
			}
		}
	}
}