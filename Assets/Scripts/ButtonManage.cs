using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManage : MonoBehaviour
{

	public Button[] buttons;
	public GameObject cursor;

	private int highlightedButton=0;

	void Update(){

	}

    public void RetryBtn()
    {
		SceneManager.LoadScene(Global.deathScene.name);
	}

	public void LoadLevelBtn(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void LoadLevelBtn(int sceneID) // build index parametrina, voi olla hyödyllinen
	{
		SceneManager.LoadScene(sceneID);
	}

    public void StartGameBtn()
    {
    }
	
    public void ExitGameBtn()
    {
        Application.Quit();
    }

	public void test29(){
		//buttontwo.GetComponent<Button> ().onClick.Invoke ();
	}

	public void test30(){
		Debug.Log ("jeeee");
	}
}
