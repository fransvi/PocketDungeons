using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManage : MonoBehaviour
{
    private Scene scene;

    public void Start()
    {
        scene = SceneManager.GetActiveScene();
    }
    public void NewGameBtn()
    {
        SceneManager.LoadScene(scene.name);
    }

    public void LoadLevelBtn()
    {
        SceneManager.LoadScene("World1");
    }

    public void StartGameBtn()
    {
        SceneManager.LoadScene("Level1.1");
    }
	
    public void ExitGameBtn()
    {
        Application.Quit();

    }
}
