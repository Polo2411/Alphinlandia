using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_RW : MonoBehaviour
{
    public GameObject Constance;
    
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.x = Constance.transform.position.x;
        position.y = Constance.transform.position.y;
        transform.position = position;

    }
}
