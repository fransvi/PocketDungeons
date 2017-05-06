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
            //KOMMENTOITU ULOS, RIKKOO KOODIN. 3.5.2017/TONI
            other.GetComponent<PlayerController>().Hurt(_spikeDamage);
        }

    }

}
