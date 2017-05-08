using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_King_Shield : MonoBehaviour {

	Bounds playerBounds;
	Animator animator;

	public int _meleeDamage;
	public Renderer player;
	public GameObject playerGO;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		//StartCoroutine (Shield ());
	}

	// Update is called once per frame
	void Update () {
		if (playerGO.transform.position.y > transform.position.y) {
			if (!animator.GetBool ("ShieldHigh")) {
				animator.SetBool ("ShieldHigh", true);
			}
		} else {
			if (animator.GetBool ("ShieldHigh")) {
				animator.SetBool ("ShieldHigh", false);
			}
		}

		//Debug.Log ("Shield: " +gameObject.GetComponent<Renderer> ().bounds);
		//Debug.Log ("Player: " +player.bounds);
		playerBounds=new Bounds(player.transform.position, new Vector3(1,1,0));
		if (gameObject.GetComponent<Renderer> ().bounds.Intersects (playerBounds)) {
			if (player.gameObject.GetComponent<PlayerController>())
			{
                //KOMMENTOITU ULOS, RIKKOO KOODIN. 3.5.2017/TONI
                player.gameObject.GetComponent<PlayerController>().Hurt(_meleeDamage);
            }
        }
	}
	/*
	IEnumerator Shield(){
		while (true) { 
			if (playerGO.transform.position.y > transform.position.y) {
				if (!animator.GetBool ("ShieldHigh")) {
					animator.SetBool ("ShieldHigh", true);
					yield return new WaitForSeconds (1);
				}
			} else {
				if (animator.GetBool ("ShieldHigh")) {
					animator.SetBool ("ShieldHigh", false);
					yield return new WaitForSeconds (1);
				}
			}
		}
	}
	*/

}
