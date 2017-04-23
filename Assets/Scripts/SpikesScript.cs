using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesScript : MonoBehaviour {

    [SerializeField]
    private int _spikeDamage;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().Hurt(_spikeDamage);
        }

    }

}
