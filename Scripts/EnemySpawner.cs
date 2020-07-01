using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 11f;
	public float height = 5f;
	public float spawnTime = 0.5f;
	public float speedFactor = 1f;

	private float rightEdge;
	private float leftEdge;
	private float xPosDelta = 0f;
	private static bool MoveRight = true;

	// Use this for initialization
	void Start () {
		xPosDelta = (Time.deltaTime * speedFactor);
	}
	
	// Update is called once per frame
	void Update () {
		if (CanvasScript.IsGamePaused) {
			Time.timeScale = 0f;
		}else{
			Time.timeScale = 1f;
			MoveFormation ();
		}
	}

	// to draw the rectangle (cube but we are in a 2d plan) surrounding the spawning positions of enimies
	void OnDrawGizmos(){
		Gizmos.DrawWireCube (this.transform.position, new Vector3 (width, height, 0));
	}

	// un-used older function of moving the enemy
	void MoveFormationOld(){
		xPosDelta = (Time.deltaTime * speedFactor);
		foreach (Transform child in this.transform) {
			Transform enemy = child.GetChild(0);
			if(enemy.transform.position.x >= PlayerOneScript.xMax){
				MoveRight = false;
			}
			if(enemy.transform.position.x <= PlayerOneScript.xMin){
				MoveRight = true;
			}
			if(MoveRight){
				enemy.transform.position += new Vector3(xPosDelta, 0, 0);
			}else{
				enemy.transform.position -= new Vector3(xPosDelta, 0, 0);
			}
		}
	}

	// the function used to move the enemy formation
	void MoveFormation(){
		rightEdge = this.transform.position.x + (width * 0.5f);
		leftEdge = this.transform.position.x - (width * 0.5f);
		if(rightEdge >= PlayerOneScript.xMax ){
			MoveRight = false;
		}else if(leftEdge <= PlayerOneScript.xMin){
			MoveRight = true;
		}
		if (MoveRight) {
			this.transform.position += new Vector3 (xPosDelta, 0, 0);
		} else {
			this.transform.position -= new Vector3(xPosDelta, 0, 0);
		}
	}

	// to ckeck if all the formation are dead
	public bool AreAllFormationDead(){
		foreach (Transform child in this.transform) {
			if(child.childCount > 0){
				return false;
			}
		}
		return true;
	}

	// used to spawn eneimes without in increase in their difficulty
	public void SpawnEnimies(){
		foreach (Transform child in this.transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.GetComponent<SpriteRenderer>().sprite = enemy.GetComponent<Enemy>().spritesArray[0];
			enemy.transform.position = child.transform.position;
			enemy.transform.parent = child.transform;
		}
	}

	// used to spawn eneimes without in increase in their difficulty but one enemy at a time
	// it's not used since it breaks the waves coming when a wave is defeated duo to the delay it forces in the invoke part 
	public void SpawnOneByOne(){
		Debug.Log ("Wave 1");
		Transform nextPos = this.NextFreePosition ();
		if(nextPos){
			GameObject enemy = Instantiate(enemyPrefab, nextPos.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = nextPos;
		}
		if (this.NextFreePosition ()) {
			Invoke ("SpawnOneByOne", spawnTime);
		}
	}

	// returns the next free position to spawn an enemy
	Transform NextFreePosition(){
		foreach(Transform child in this.transform){
			if(child.childCount == 0){
				return child;
			}
		}
		return null;
	}

	// the used fuction to spawn enimies
	// it increase difficulty with each wave of eneimes
	public void SpawnEnemiesHarder(){
		if (SpawnerController.wavesSurvived > 3) {
			xPosDelta = xPosDelta + (0.2f * xPosDelta);
			foreach (Transform child in this.transform) {
				GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
				enemy.GetComponent<SpriteRenderer>().sprite = enemy.GetComponent<Enemy>().spritesArray[0];
				enemy.GetComponent<Enemy>().shotVelocity += 0.5f;
				enemy.GetComponent<Enemy>().health += 50f;
				enemy.transform.position = child.transform.position;
				enemy.transform.parent = child.transform;
			}
		} else {
			SpawnEnimies();
		}
	}
}
