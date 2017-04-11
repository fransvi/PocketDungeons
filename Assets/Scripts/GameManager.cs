using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//TODO Generate enemy spawns ect.
	}

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void LoadMenu()
    {
        Application.LoadLevel("GameMenu");
    }
}
