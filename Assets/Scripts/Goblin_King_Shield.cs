using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_King_Shield : MonoBehaviour {

	Bounds playerBounds;

	public int _meleeDamage;
	public Renderer player;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log ("Shield: " +gameObject.GetComponent<Renderer> ().bounds);
		//Debug.Log ("Player: " +player.bounds);
		playerBounds=new Bounds(player.transform.position, new Vector3(1,1,0));
		if (gameObject.GetComponent<Renderer> ().bounds.Intersects (playerBounds)) {
			Debug.Log ("nice");
			if (player.gameObject.GetComponent<PlayerController>())
			{
				player.gameObject.GetComponent<PlayerController>().Hurt(_meleeDamage);
			}
		}
	}
}
