using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Button endGameButton;
    private void Start()
    {
        endGameButton.onClick.AddListener(OneEndGameButtonClicked);
    }
    //starting the game again
    public void OnStartAgainButtonClicked()
    {
        SceneManager.LoadScene("SceneOne");
        Debug.Log("Start Again");
    }
    //ending the game
    private void OneEndGameButtonClicked()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }
}