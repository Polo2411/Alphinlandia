using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleReloj : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject dialogCanvas;
    public GameObject inputCanvas;
    
    private string input;
    public Bookmenu bookMenu;
    public Helper helper;
    private bool inCollision = false;
    private bool hasInteracted = false;
    private bool firstTimeInteracting = true;
    private bool puzzleSolved = false;

    private string correctAnswer = "18:30";
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
        else if (Input.GetKeyDown(KeyCode.Space)){
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
            ("Constance", "Aqui esta el reloj, pero la hora no esta bien.", Color.blue),
            ("Ewan", "Ese reloj siempre marcava una hora en especifico muy importante.", Color.green),
            ("Constance", "No la recuerdo... Como puedo no recordarla?!", Color.blue),
            ("Ewan", "No te pongas nerviosa. Prueba a escribir en Alphinlandia eso siempre te ha ayudado a recordar.", Color.green),
            ("Constance", "Tienes razon, voy a intentarlo.", Color.blue)
        };
    }
    void ChangePuzzleLines()
    {
        textComponent.text = string.Empty;
        puzzleLines = new (string, string, Color)[]
        {
            ("Constance", "Se ha abierto el reloj y parece que hay una nota.", Color.blue),
            ("Constance", "...", Color.blue),
            ("Constance", "Dice que hay algo muy importante en el armario de Olivia.", Color.blue),
            ("Constance", "¿Qué crees que puede ser?", Color.blue),
            ("Ewan", "¿Recuerdas qué hay en el armario?", Color.green),
            ("Constance", "No lo recuerdo, ¿tú sí?", Color.blue),
            ("Ewan", "No puedo decírtelo. Debes descubrirlo por ti misma.", Color.green),
            ("Ewan", "La llave de la habitación podría estar en el cajón de la cocina, junto a los imanes con las fotos de nuestra boda.", Color.green),
            ("Constance", "Otra vez von tus misticismos, ¡Eres un pesado! Déjame en paz.", Color.blue),
            ("Ewan", "Está bien, ya no te molesto más.", Color.green)
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
                if (bookMenu.currentMemoryIndex < 2 && helper.actualDialog < 2)
                {
                    bookMenu.currentMemoryIndex = 2;
                    helper.actualDialog = 2;
                }
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
