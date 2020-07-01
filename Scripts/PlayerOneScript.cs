using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerOneScript : MonoBehaviour {	
	public  float Speed = 10f;
	public GameObject laserPrefab;
	public float projectileSpeed = 5f;
	public float fireRate = 0.2f;   // 0.2 seconds
	public AudioClip playerFireSound;
	public static float xMin;
	public static float xMax; 
	public static Sprite PlayerShip = null;

	private Vector2 newPos;
	private Score healthKeeper;
	private static float PlayerOneXPos = 0;
	private static float padding = 0.75f;
	private bool playerHasFireRateBoast = false;

	// Use this for initialization
	void Start () {
		if(PlayerShip){
			this.GetComponent<SpriteRenderer>().sprite = PlayerShip;
		}
		newPos = new Vector2 (0f, this.transform.position.y);
		GetScreenBoundries ();  // instantiates xMin and xMax 
		healthKeeper = GameObject.Find ("Score").GetComponent<Score> ();
	}
	
	// Update is called once per frame
	void Update () {
		this.HandleKeyboardMovement ();
		this.ForceFireRate ();
	}
	
	// to move the ship object when the player presses left or right arrow
	void HandleKeyboardMovement(){
		if(Input.GetKey(KeyCode.RightArrow)){
			if(CanMoveRight(PlayerOneXPos)){
				PlayerOneXPos += (Speed * Time.deltaTime);
				newPos.x = PlayerOneXPos;
				this.transform.position = newPos;
			}
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			if(CanMoveLeft(PlayerOneXPos)){
				PlayerOneXPos -= (Speed * Time.deltaTime);
				newPos.x = PlayerOneXPos;
				this.transform.position = newPos;
			}
		}
	}

	// to prevent player form going out of the screen in the x directin
	void GetScreenBoundries(){
		float zDistance = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMostPos = Camera.main.ViewportToWorldPoint (new Vector3(0, 0, zDistance));
		Vector3 rightMostPos = Camera.main.ViewportToWorldPoint (new Vector3(1, 0, zDistance));
		xMin = leftMostPos.x + padding;
		xMax = rightMostPos.x - padding;
	}

	// returnes true if the player can move laterally in the right direction without going out of the scrren
	bool CanMoveRight(float pos){
		return (pos < xMax);
	}

	// returnes true if the player can move laterally in the left direction without going out of the scrren
	bool CanMoveLeft(float pos){
		return (pos > xMin); 
	}

	void Fire(){
		GameObject laserProjectile = Instantiate(laserPrefab, this.transform.position, Quaternion.identity) as GameObject;
		laserProjectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, projectileSpeed, 0f);
		if(MusicToggleScript.SoundEffects){
			AudioSource.PlayClipAtPoint (playerFireSound, this.transform.position, 0.2f);
		}
	}

	void ForceFireRate(){
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire", 0.000001f, this.fireRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.GetComponent<Laser>()){
			Laser laser = col.gameObject.GetComponent<Laser>();
			this.healthKeeper.DecreaseHealthBy((int)laser.damage);
			laser.hit();
			if(healthKeeper.PlayerHealthCount<= 0){
				GameObject.Find("LevelManger").GetComponent<LevelMangerScript>().GoTo("Game_Over");
			}
		}else if(col.gameObject.GetComponent<Health>()){
			this.healthKeeper.IncreaseHealthBy(100);
		}else if(!playerHasFireRateBoast && col.gameObject.GetComponent<FireRateBoast>()){
			playerHasFireRateBoast = true;
			this.fireRate = this.fireRate / 3;
			this.projectileSpeed = this.projectileSpeed * 3;
			CancelInvoke("Fire");
			InvokeRepeating("Fire", 0.000001f, this.fireRate);
			Invoke("ResetFireRate", 3f);
		}
		Destroy (col.gameObject);
	}

	void ResetFireRate(){
		playerHasFireRateBoast = false;
		this.fireRate = 0.2f;
		this.projectileSpeed = 5f;
		CancelInvoke("Fire");
	}
}
