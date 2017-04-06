using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocked : MonoBehaviour {

	bool hasKey=true; // Placeholderi; Hae boolean pelaajan inventorysta
	bool doorOpened=false;
	public Renderer player;

	bool playerDoorIntersect;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		playerDoorIntersect = gameObject.GetComponent<Renderer> ().bounds.Intersects (player.bounds);
		if (hasKey==true && Input.GetKeyUp(KeyCode.LeftControl) == true && doorOpened==false && playerDoorIntersect){
			doorOpened = true;
			Debug.Log ("doorOpened");
		}
	}
}
