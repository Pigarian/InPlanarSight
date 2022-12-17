using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private SceneManager killer;
    void Start()
    {
        killer = FindObjectOfType<SceneManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>()) killer.Death();
    }
}
