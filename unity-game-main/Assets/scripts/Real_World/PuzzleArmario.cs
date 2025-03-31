using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PuzzleArmario : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject dialogCanvas;
    public GameObject inputCanvas;

    private string input;
    public Helper helper;
    private bool inCollision = false;
    private bool hasInteracted = false;
    private bool firstTimeInteracting = true;
    private bool puzzleSolved = false;

    private string correctAnswer = "23041509Olivia";
    public float textSpeed;
    public bool dialogFinished = false;
    public static bool PuzzleOpen = false;
    [SerializeField] private AudioSource sound;
    public (string, string, Color)[] puzzleLines;
    private int index = 0;

    void Start()
    {
        // Initialize puzzle dialog lines
        InitializePuzzleLines();
        textComponent.text = string.Empty;
        // Ensure both canvases are inactive at the start
        dialogCanvas.SetActive(false);
        inputCanvas.SetActive(false);
    }

    void Update()
    {
        // Check if the space bar is pressed when the player is in collision and has not interacted yet
        if (Input.GetKeyDown(KeyCode.Space) && inCollision && puzzleSolved)
        {
            if (hasInteracted)
            {
                if (textComponent.text == puzzleLines[index].Item2)
                {
                    NextLine();

                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = puzzleLines[index].Item2;

                }
            }
            else
            {
                hasInteracted = true;
                textComponent.text = string.Empty;
                OpenDialog();
            }


        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inCollision && !firstTimeInteracting)
            {
                inputCanvas.SetActive(true);
            }
            else if (inCollision && !hasInteracted && !dialogFinished)
            {
                hasInteracted = true;
                OpenDialog();
            }
            else if (hasInteracted)
            {
                if (textComponent.text == puzzleLines[index].Item2)
                {
                    NextLine();

                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = puzzleLines[index].Item2;

                }
            }
        }

    }

    // Open the dialog when the player enters collision with the object
    void OpenDialog()
    {
        // Open the dialog canvas
        dialogCanvas.SetActive(true);
        StartDialog();
    }

    // Start the dialog
    void StartDialog()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    // Advance to the next dialog line
    void NextLine()
    {
        if (index < puzzleLines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogCanvas.SetActive(false);
            dialogFinished = true;
            PuzzleOpen = false;
            hasInteracted = false;
            if (!puzzleSolved)
            {
                inputCanvas.SetActive(true);
            }
        }
    }

    // Handle collision with player object
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inCollision = true;
        }
    }

    // Handle exit from collision with player object
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inCollision = false;
        }
    }

    // Initialize puzzle dialog lines
    void InitializePuzzleLines()
    {
        // Define puzzle dialog lines here
        puzzleLines = new (string, string, Color)[]
        {
            ("Ewan", "Sabes el codigo del armario y de la cajita que hay dentro?", Color.green),
            ("Constance", "No sé si lo recuerdo...", Color.blue),
            ("Ewan", "Recuerda que debes introducir los dos seguidos para que se pueda abrir.", Color.green),
            ("Constance", "Lo intentare...", Color.blue)
        };
    }
    void ChangePuzzleLines()
    {
        textComponent.text = string.Empty;
        puzzleLines = new (string, string, Color)[]
        {
            ("Constance", "Se ha abierto el armario y la cajita...", Color.blue),
            ("Constance", "...", Color.blue),
            ("Constance", "Parece que hay una carta escrita por tí...", Color.blue),
            ("Ewan", "Leela...", Color.green),
            ("Constance", "...", Color.blue),
            ("Constance", "Yo también he sido muy feliz de vivir esta vida contigo...", Color.blue),
            ("Ewan", "Venga va! No llores cariño...", Color.green),
            ("Constance", "...", Color.blue),
            ("Constance", "Creo que ya estoy preparada...", Color.blue),
            ("Ewan", "Mi vida, és hora de dejarme ir y entrar en la habitación...", Color.green),
            ("Constance", "...", Color.blue)
    };
    }

    IEnumerator TypeLine()
    {
        string characterName = puzzleLines[index].Item1;
        string dialogue = puzzleLines[index].Item2;
        Color textColor = puzzleLines[index].Item3;

        // Cambia el color del texto
        textComponent.color = textColor;

        foreach (char c in dialogue.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        // Restaura el color del texto a blanco al finalizar la l?nea
        textComponent.color = Color.white;

        NextLine();
    }

    // Function to check the user's input and solve the puzzle
    public void CheckAnswer(string userInput)
    {
        input = userInput;
        Debug.Log(input);
        if (!puzzleSolved)
        {
            firstTimeInteracting = false;
            if (input == correctAnswer)
            {
                helper.actualDialog = 9;
                sound.Play();
                Debug.Log("Puzzle solved!");
                // Add code to handle puzzle solved scenario
                puzzleSolved = true;
                // Close the input canvas
                inputCanvas.SetActive(false);
                ChangePuzzleLines();
                hasInteracted = true;
                OpenDialog();
            }
            else
            {
                inputCanvas.SetActive(false);
            }
        }
    }
}
