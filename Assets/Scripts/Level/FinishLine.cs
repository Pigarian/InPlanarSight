using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private bool lastLevel = false;
    [SerializeField] private GameObject endOptions;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.GetComponent<Player>()) return;
        if (!lastLevel) FindObjectOfType<SceneManager>().NextScene();
        else
        {
            endOptions.SetActive(true);
            FindObjectOfType<SceneManager>().curScene = 5;
            FindObjectOfType<Timer>().StopClock();
            Cursor.visible = true;
        }
    }

    public void FinishQuit()
    {
        FindObjectOfType<SceneManager>().Quit();
    }

    public void FinishReturn()
    {
        FindObjectOfType<SceneManager>().ReturnToMenu();
    }
}
