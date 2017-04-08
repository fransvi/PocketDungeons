using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    // Use this for initialization
    void Start () {
		Global.deathScene = SceneManager.GetActiveScene();
		StartCoroutine (DeathAnimation ());
	}

	IEnumerator DeathAnimation()
	{
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene ("GameOver");
	}
	
}