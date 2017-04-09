using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesScript : MonoBehaviour {

    [SerializeField]
    private int _spikeDamage;

    public GameObject player;
	// Use this for initialization
	void Start () {

        player = GameObject.Find("Player");
	}


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == "Player")
        {
            player.GetComponent<PlayerController>().Hurt(_spikeDamage);
        }

    }

}
