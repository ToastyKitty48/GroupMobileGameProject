using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    Canvas pauseCan; // Pause menu UI canvas
    Canvas gameUICan; //Player UI canvas
    void Start()
    {
        //gets canvases of the pause menu & game UI
        pauseCan = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<Canvas>();
        gameUICan = GameObject.FindGameObjectWithTag("GameUICanvas").GetComponent<Canvas>();
        pauseCan.enabled = false; //Turns off pause menu canvas at start
    }
    //Pauses the game
    public void Pause()
    {
        pauseCan.enabled = true;
        gameUICan.enabled = false;
        Time.timeScale = 0;
    }
    //Resumes the game
    public void Resume()
    {
        pauseCan.enabled = false;
        gameUICan.enabled = true;
        Time.timeScale = 1;
    }
    //Restarts curent level
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    //Quits game
    public void QuitGame()
    {
        Application.Quit();
    }
}
