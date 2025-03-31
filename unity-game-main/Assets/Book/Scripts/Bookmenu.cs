using UnityEngine;
using UnityEngine.UI;

public class Bookmenu : MonoBehaviour
{
    public GameObject bookMenu;
    public Image bookImage; // Reference to the Image component of the book
    public bool isPaused;

    public int currentMemoryIndex = 0; // Variable to store the current memory index

    // Sprites for each memory
    public Sprite[] memorySprites;

    private void Start()
    {
        bookMenu.SetActive(false);
    }

    public void PauseGame()
    {
        bookMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Update the book sprite based on the current memory index
        UpdateBookSprite();
    }

    public void ResumeGame()
    {
        bookMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Function to update the book sprite based on the current memory index
    private void UpdateBookSprite()
    {
        // Check if the current memory index is within bounds of the memorySprites array
        if (currentMemoryIndex >= 0 && currentMemoryIndex < memorySprites.Length)
        {
            // Update the book sprite with the corresponding memory sprite
            bookImage.sprite = memorySprites[currentMemoryIndex];
        }
    }
}
