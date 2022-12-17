using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Start()
    {
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private void Update()
    {
        if (player.transform.localScale.y < 0.75f) player.transform.localScale += new Vector3(0, .05f, 0);
        else
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 5;
            player.GetComponent<Player>().canInput = true;
            gameObject.GetComponent<Spawn>().enabled = false;
        }
    }
}
