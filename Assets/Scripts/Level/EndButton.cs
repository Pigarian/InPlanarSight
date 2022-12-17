using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButton : MonoBehaviour
{
    private SubLevelHandler caller;
    void Start()
    {
        caller = FindObjectOfType<SubLevelHandler>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>()) caller.Next();
    }
}
