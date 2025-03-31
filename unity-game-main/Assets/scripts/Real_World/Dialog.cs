using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed;
    private int index;
    public (string, string, Color)[] dialogueLines;
    public int actualDialog = 0;
    public bool dialogFinished = false;

    public static bool dialogOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        LoadDialoguesForScene();
        textComponent.text = string.Empty;
        dialogOpen = true;
        StartDialog();
    }

    void LoadDialoguesForScene()
    {
        switch (actualDialog)
        {
            case 0:
                dialogueLines = new (string, string, Color)[]
                {
                    ("Constance", "Parece que se viene una gran tormenta.", Color.blue),
                    ("Ewan", "Constance, ¿Sigues con el reloj que te regalé?", Color.green),
                    ("Ewan", "Está en la mesita de noche de la habitación de invitados, donde duermes des de que...", Color.green),
                    ("Constance", "No sigas... Ahora lo buscare, pesado.", Color.blue)
                };
                break;
            case 1:
                dialogueLines = new (string, string, Color)[]
                {
                    ("Ewan", "Constance, ¿te ha servido escribir?", Color.green),
                    ("Constance", "Supongo que sí.", Color.blue),
                    ("Ewan", "Recuerdas ya la hora que marcava?", Color.green),
                    ("Constance", "Eso creo.", Color.blue),
                };
                break;
            case 2:
                dialogueLines = new (string, string, Color)[]
                {
                    ("Ewan", "Que divertido fue el dia de nuestra boda. Recuerdo cuando tu hermana... JAJAJAJA!", Color.green),
                    ("Constance", "Ay si, nos reimos mucho cuando se cayo por cojer el ramo JAJAJAJAJAJA!", Color.blue),
                    ("Ewan", "Hace mucho que no te veía reír así. Me encanta escuchar esa risa.", Color.green),
                    ("Constance", "...", Color.blue)
                };
                break;
            case 3:
                dialogueLines = new (string, string, Color)[]
                {
                    ("Ewan", "Recuerdo el primer libro que escribiste. Fue un hito.", Color.green),
                    ("Constance", "Sí, fue una experiencia única.", Color.blue),
                    ("Ewan", "No era muy fácil de seguir, pero le tengo mucho cariño.", Color.green),
                    ("Constance", "Recuerdo una hada muy secundaria en una página que me gustaba mucho. Era muy traviesa.", Color.blue)
                };
                break;
            case 4:
                dialogueLines = new (string, string, Color)[]
                {
                    ("Ewan", "¿Recuerdas esa canción, Constance?", Color.green),
                    ("Constance", "Sí, cómo olvidarla. Siempre me emociona escucharla.", Color.blue),
                    ("Ewan", "¿Podría tocarla en el piano?", Color.green),
                    ("Constance", "Claro, sería maravilloso.", Color.blue)
                };
                break;
            default:
                dialogueLines = new (string, string, Color)[]
                {
                    ("Ewan", "¿Recuerdas esa canción, Constance?", Color.green),
                    ("Constance", "Sí, cómo olvidarla. Siempre me emociona escucharla.", Color.blue),
                    ("Ewan", "¿Podría tocarla en el piano?", Color.green),
                    ("Constance", "Claro, sería maravilloso.", Color.blue)
                };
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.text == dialogueLines[index].Item2)
            {
                NextLine();

            }
            else
            {
                if (dialogOpen)
                {
                    StopAllCoroutines();
                    textComponent.text = dialogueLines[index].Item2;
                }
            }
        }
    }

    void StartDialog()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    void NextLine()
    {
        if (index < dialogueLines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            if (dialogFinished)
            {
                actualDialog += 1;
                PlayerPrefs.SetInt("ActualDialog", actualDialog);
            }
            dialogFinished = true;
            dialogOpen = false;


        }
    }

    IEnumerator TypeLine()
    {
        string characterName = dialogueLines[index].Item1;
        string dialogue = dialogueLines[index].Item2;
        Color textColor = dialogueLines[index].Item3;

        // Cambia el color del texto
        textComponent.color = textColor;

        foreach (char c in dialogue.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        // Restaura el color del texto a blanco al finalizar la línea
        textComponent.color = Color.white;

        NextLine();
    }

}
