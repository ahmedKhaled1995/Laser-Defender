using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	public float damage = 100;
	//public bool enemyShot = false;   // if true then the projectile is fired by the enemy

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void hit(){
		Destroy (this.gameObject);
	}
}
