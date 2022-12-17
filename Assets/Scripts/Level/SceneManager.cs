using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    public int curScene;
    public bool beenTwo = false;
    public bool beenThree = false;
    private bool lateStart = false;
    private bool inEscape = false;
    public int curSub;

    private void Start()
    {
        curSub = 0;
        curScene = 0;
        DontDestroyOnLoad(gameObject);
    }

    private void LateStart()
    {
        Cursor.visible = false;
        lateStart = true;
    }

    private void Update()
    {
        if (curScene == 0 || curScene == 5) return;
        if (!lateStart) LateStart();
        if (Input.GetKeyDown(KeyCode.Escape)) EscapeMenu();
    }

    public void NextScene()
    {
        curScene++;
        UnityEngine.SceneManagement.SceneManager.LoadScene(curScene);
    }

    public void Death()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(curScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        curScene = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene(curScene);
        Destroy(gameObject);
    }
    
    private void EscapeMenu()
    {
        inEscape = !inEscape;
        Cursor.visible = inEscape;
        FindObjectOfType<Player>().canInput = !inEscape;
        menu.SetActive(inEscape);
    }
}
