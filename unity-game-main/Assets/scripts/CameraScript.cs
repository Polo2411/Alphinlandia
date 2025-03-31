using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Constance;
    public float value = 3;
    public float zoomFactor = 2f; // Adjust this value to control the zoom level

    void Update()
    {   
        Vector3 position = transform.position;
        position.x = Constance.transform.position.x;
        position.y = Constance.transform.position.y + value;
        transform.position = position;

        // Adjust the orthographic size to zoom out
        Camera.main.orthographicSize = zoomFactor;
    }
}

