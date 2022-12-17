using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private AudioSource goSound;
    [SerializeField] private GameObject ps;
    [SerializeField] private GameObject lilGuy;
    private Vector3 moveGoal;
    private System.Random rng;
    private int moves;
    private bool lastCountDown = false;
    private bool once = false;
    private bool anotherOnce = false;
    private void Start()
    {
        rng = new System.Random(Mathf.CeilToInt(Time.time));
        moveGoal = new Vector3(rng.Next(-1, 2), rng.Next(-1, 2), 0);
        moves = 0;
    }

    void Update()
    {
        gameObject.transform.Rotate(0,0,-.1f);
        gameObject.transform.position += moveGoal * (0.1f * Time.deltaTime);
        moves++;
        if (moves > 2000)
        {
            moveGoal = new Vector3(rng.Next(-1, 2), rng.Next(-1, 2), 0);
            moves = 0;
        }

        if (lastCountDown)
        {
            if (!once)
            {
                Instantiate(ps, gameObject.transform);
                goSound.Play();
                once = true;
            }
            if (lilGuy.transform.localScale.y > 0f) lilGuy.transform.localScale -= new Vector3(0, .1f, 0);
            else if (!anotherOnce)
            {
                anotherOnce = true;
                StartCoroutine(GoNext());
            }
        }
    }

    public void StartGame()
    {
        lastCountDown = true;
    }

    IEnumerator GoNext()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<SceneManager>().NextScene();
    }
    
}
