using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private SceneController sceneController;

    public void StartGame()
    {
        sceneController.LoadScene("Game");
    }
    public void StartTutorial()
    {
        sceneController.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
