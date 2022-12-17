using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFix : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 7.6) transform.position = new Vector3(7.6f, transform.position.y, transform.position.z);
        if (transform.position.x < -7.6) transform.position = new Vector3(-7.6f, transform.position.y, transform.position.z);
    }
}
