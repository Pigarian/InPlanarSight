using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private RectTransform timerLocation;
    [SerializeField] private Text timer;
    private float startTime;
    private bool stop;
    private string minutes;
    private string seconds;
    private float t;
    
    // Start is called before the first frame update
    void Start()
    {
        stop = false;
        startTime = Time.time;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<SceneManager>().curScene == 0) Destroy(gameObject);
        if (stop) return;
        t = Time.time - startTime;
        minutes = ((int)t / 60).ToString();
        seconds = (t % 60).ToString("f1");
        if (t % 60 < 10) timer.text = minutes + ":0" + seconds;
        else timer.text = minutes + ":" + seconds;
    }

    public void StopClock()
    {
        stop = true;
        timerLocation.anchoredPosition = new Vector2(0, -210);
        string timePart;
        if (t < 10) timePart = minutes + ":0" + seconds;
        else timePart = minutes + ":" + seconds;
        timer.text = "YOUR TIME:\n" + timePart;
    }
}
