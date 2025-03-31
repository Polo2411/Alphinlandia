using UnityEngine;
using UnityEngine.SceneManagement;

public class Alph_SceneChanger : MonoBehaviour
{
    public string sceneName;
    private bool inCollision = false;

    void Update()
    {
        // Check if the space bar is pressed and inCollision flag is true
        if (Input.GetKeyDown(KeyCode.E) && inCollision)
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneName);
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
