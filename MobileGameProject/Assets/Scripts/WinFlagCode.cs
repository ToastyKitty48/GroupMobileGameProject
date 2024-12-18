using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinFlagCode : MonoBehaviour

{
    Canvas LevelCompleteCan;
    void Start()
    {
        LevelCompleteCan = GameObject.FindGameObjectWithTag("LevelCompleteCanvas").GetComponent<Canvas>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LevelCompleteCan.enabled = true;
            Time.timeScale = 0;
        }
    }
}
