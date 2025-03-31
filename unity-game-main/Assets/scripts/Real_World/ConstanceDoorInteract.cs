using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstanceDoorInteract : MonoBehaviour
{
    public GameObject door;
    public Helper helper;
    private bool inCollision = false;
    public int opened = 0;
    [SerializeField] private AudioSource sound;
    private void Awake()
    {
        if (opened == 1)
        {
            Destroy(door);
        }
    }

    void Update()
    {
        // Check if the space bar is pressed and inCollision flag is true
        if (Input.GetKeyDown(KeyCode.Space) && inCollision)
        {
            if (helper.actualDialog >= 9)
            {
                sound.Play();
                opened = 1;
                Destroy(door);
            }
        }

    }

    // Set inCollision to true when colliding with the object
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inCollision = true;
        }
    }

    // Set inCollision to false when no longer colliding with the object
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inCollision = false;
        }
    }
}
