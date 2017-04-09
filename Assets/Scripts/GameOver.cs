using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    // Use this for initialization
    void Start () {
	}

	void InitiateGameOver(){
		// health bar -skripti kutsuu tätä funktiota
		Global.deathScene = SceneManager.GetActiveScene().name;
		StartCoroutine (DeathAnimation ());
	}

	IEnumerator DeathAnimation()
	{
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene ("GameOver");
	}
	
}