using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float health = 150;
	public GameObject enemyLaser;
	public float shotVelocity = 5f;
	public float lootVelocity;
	public float shotsPerSecond = 0.5f;
	public Sprite [] spritesArray;
	public AudioClip enemyFireSound;
	public AudioClip enemyDestructionSound;
	public GameObject healthBoastPrefab;
	public GameObject fireRateIncreasePrefab;

	private Score scoreKeeper;
	private float probability = 1;
	private int scoreToAdd = 100;
	
	// Use this for initialization
	void Start () {
		scoreKeeper = GameObject.Find ("Score").GetComponent<Score>();
		lootVelocity = shotVelocity / 2f;
	}
	
	// Update is called once per frame
	void Update () {
		probability = Time.deltaTime * shotsPerSecond;
		if(Random.value < probability){
			this.EnemyFire ();
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		Laser laser = collider.gameObject.GetComponent<Laser>();
		if(laser ){   //if(laser && !laser.enemyShot)
			this.health -= laser.damage;
			laser.hit();
			if(this.health <= 0){
				this.DropLoot();
				this.Die();
			}
		}
	}

	void EnemyFire(){
		GameObject shot = Instantiate (enemyLaser, this.transform.position, Quaternion.identity) as GameObject;
		shot.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -this.shotVelocity);
		if(MusicToggleScript.SoundEffects){
			AudioSource.PlayClipAtPoint(enemyFireSound, this.transform.position, 0.2f);
		}
	}

	
	void Die(){
		Destroy(this.gameObject);
		scoreKeeper.addScore (scoreToAdd);
		if(MusicToggleScript.SoundEffects){
			AudioSource.PlayClipAtPoint(enemyDestructionSound, this.transform.position, 0.5f);
		}
	}

	void DropLoot(){
		int random1 = Random.Range (1, 6);  // returns a number between 1 and 5
		if(random1 == 1){
			int random2 = Random.Range (1, 5);   // returns a number between 1 and 4
			if(random2 == 1){
				GameObject healthBoast = Instantiate(healthBoastPrefab, this.transform.position, Quaternion.identity) as GameObject;
				healthBoast.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -lootVelocity);
			}else if (random2 > 1){
				GameObject increaseFireRate = Instantiate(fireRateIncreasePrefab, this.transform.position, Quaternion.identity) as GameObject;
				increaseFireRate.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -lootVelocity);
			}
		}
	}
}
