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

    public void LoadGameOverScreen()
    {

    }

    public void LoadLevel1_2()
    {
        StartCoroutine(ChangeLevel());
    }
    IEnumerator ChangeLevel()
    {
        float fadeTime = GetComponent<AutoFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void LoadMenu()
    {
        Application.LoadLevel("GameMenu");
    }
}
