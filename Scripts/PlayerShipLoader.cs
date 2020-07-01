using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerShipLoader : MonoBehaviour {
	public Sprite[] spriteArray;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClicked(Image img)
	{
		//RedSpaceShip
		//BlueSpaceShip
		//GreenSpaceShip
		//print(img.name);
		if(img.name == "RedSpaceShip"){
			PlayerOneScript.PlayerShip = spriteArray[0];
		}else if(img.name == "BlueSpaceShip"){
			PlayerOneScript.PlayerShip = spriteArray[1];
		}else if(img.name == "GreenSpaceShip"){
			PlayerOneScript.PlayerShip = spriteArray[2];
		}
	}


}
