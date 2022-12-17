using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    [SerializeField] private GameObject[] buttonWalls;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("cube")) for (int i = 0; i < 2; i++) buttonWalls[i].SetActive(!buttonWalls[i].activeSelf);
    }
}
