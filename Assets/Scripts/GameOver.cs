using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    private string sceneBuildIndex;
    private Scene scene;

    // Use this for initialization
    void Start () {
         scene = SceneManager.GetActiveScene();
        Debug.Log("Active scene is '" + scene.name + "'.");
	}
	

    public void load()
    { Application.LoadLevel(scene.name);
        Debug.Log("testi");
            }
	
}