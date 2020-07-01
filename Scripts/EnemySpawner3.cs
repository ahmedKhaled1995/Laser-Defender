using UnityEngine;
using System.Collections;

public class EnemySpawner3 : MonoBehaviour {
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
		if(CanvasScript.IsGamePaused){
			Time.timeScale = 0f;
		}else{
			Time.timeScale = 1f;
			MoveFormation ();
		}
	}
	
	void OnDrawGizmos(){
		Gizmos.DrawWireCube (this.transform.position, new Vector3 (width, height, 0));
	}

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

	void MoveFormation(){
		rightEdge = this.transform.position.x + (width * 0.5f);
		leftEdge = this.transform.position.x - (width * 0.5f);
		if(rightEdge >= PlayerOneScript.xMax || leftEdge <= PlayerOneScript.xMin){
			MoveRight = !MoveRight;
		}
		if (MoveRight) {
			this.transform.position += new Vector3 (xPosDelta, 0, 0);
		} else {
			this.transform.position -= new Vector3(xPosDelta, 0, 0);
		}
	}

	public bool AreAllFormationDead(){
		foreach (Transform child in this.transform) {
			if(child.childCount > 0){
				return false;
			}
		}
		return true;
	}

	public void SpawnEnimies(){
		//Debug.Log ("Wave 3");
		foreach (Transform child in this.transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.GetComponent<SpriteRenderer>().sprite = enemy.GetComponent<Enemy>().spritesArray[2];
			enemy.GetComponent<Enemy>().shotVelocity = 5f;
			enemy.GetComponent<Enemy>().health = 150f;
			enemy.transform.position = child.transform.position;
			enemy.transform.parent = child.transform;
		}
	}

	// note that this method will be called when AreAllFormationDead method returns true
	public void SpawnOneByOne(){
		Debug.Log ("Wave 3");
		Transform nextPos = this.NextFreePosition ();
		if(nextPos){
			GameObject enemy = Instantiate(enemyPrefab, nextPos.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = nextPos;
		}
		if (this.NextFreePosition ()) {
			Invoke ("SpawnOneByOne", spawnTime);
		}
	}

	Transform NextFreePosition(){
		foreach(Transform child in this.transform){
			if(child.childCount == 0){
				return child;
			}
		}
		return null;
	}

	public void SpawnEnemiesHarder(){
		if (SpawnerController.wavesSurvived > 6) {
			xPosDelta = xPosDelta + (0.2f * xPosDelta);
			foreach (Transform child in this.transform) {
				GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
				enemy.GetComponent<SpriteRenderer>().sprite = enemy.GetComponent<Enemy>().spritesArray[2];
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
