using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour {
	public GameObject formationOne;
	public GameObject formationTwo;
	public GameObject formationThree;

	private bool inWave1;
	private bool inWave2;
	private bool inWave3;
	private EnemySpawner wave1;
	private EnemySpawner2 wave2;
	private EnemySpawner3 wave3;

	public static int wavesSurvived;
	 

	// Use this for initialization
	void Start () {
		wavesSurvived = 1;
		wave1 = formationOne.GetComponent<EnemySpawner> ();
		wave2 = formationTwo.GetComponent<EnemySpawner2> ();
		wave3 = formationThree.GetComponent<EnemySpawner3> ();
		wave1.SpawnEnemiesHarder ();
		inWave1 = true;
		inWave2 = false;
		inWave3 = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(inWave1){
			if(wave1.AreAllFormationDead()){
				SpawnerController.wavesSurvived += 1;
				inWave2 = true;
				inWave1 = false;
				wave2.SpawnEnemiesHarder();
			}
		}else if(inWave2){
			if(wave2.AreAllFormationDead()){
				SpawnerController.wavesSurvived += 1;
				inWave3 = true;
				inWave2 = false;
				wave3.SpawnEnemiesHarder();
			}
		}else if(inWave3){
			SpawnerController.wavesSurvived += 1;
			if(wave3.AreAllFormationDead()){
				inWave1 = true;
				inWave3 = false;
				wave1.SpawnEnemiesHarder();
			}
		}
	}
}
