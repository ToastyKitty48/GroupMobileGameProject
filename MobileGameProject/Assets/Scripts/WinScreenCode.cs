using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenCode : MonoBehaviour
{
    [SerializeField] int LevelToLoad = 0;
    private void Start()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelToLoad);
        Time.timeScale = 1;
    }
}
