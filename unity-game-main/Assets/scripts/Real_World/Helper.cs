using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Helper : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject dialog;

    public float textSpeed;
    private int index;
    public (string, string, Color)[] helperLines;
    public int actualDialog = 0;
    public bool dialogFinished = false;
    public static bool helperOpen = false;
    void LoadDialoguesForScene()
    {
        // Debug.Log("Actual Dialogoooo: " + actualDialog);
        switch (actualDialog)
        {
            case 0:
                helperLines = new (string, string, Color)[]
                {
                    ("Ewan", "Deberías escribir Alphinlandia, en tu ordenador como haces siempre, te ayuda a recordar.", Color.green),
          
                };
                break;
            case 1:
                helperLines = new (string, string, Color)[]
                {
                    ("Ewan", "El reloj que tienes en la mesilla de noche, recuerdas la hora que marcaba?", Color.green),
                };
                break;
            case 2:
                helperLines = new (string, string, Color)[]
                {
                    ("Ewan", "La llave esta en el cajon de la cocina, recuerdo que el candado se abria con una fecha.", Color.green),
                };
                break;
            case 3:
                helperLines = new (string, string, Color)[]
                {
                    ("Ewan", "Recuerdas la fecha? Ya puedes abrir el cajon de la cocina.", Color.green),
                };
                break;
            case 4:
                helperLines = new (string, string, Color)[]
                {
                    ("Ewan", "Con la llave podras abrir la habitación de Olivia, creo que hay algo en el armario.", Color.green),
                };
                break;
            case 5:
                helperLines = new (string, string, Color)[]
                {
                    ("Ewan", "El libro que tenemos en la pequeña biblioteca, hay una pagina muy importante.", Color.green),
                };
                break;
            case 6:
                helperLines = new (string, string, Color)[]
                {
                    ("Ewan", "No creo que el armario pueda ser abierto con el nombre de Olivia, deberías volver a Alphinlandia.", Color.green),
                };
                break;
            case 7:
                helperLines = new (string, string, Color)[]
                {
                    ("Ewan", "Me encantaría volver a escuchar la canción en el piano, como era la melodía?", Color.green),
                };
                break;
            case 8:
                helperLines = new (string, string, Color)[]
                {
                    ("Ewan", "Creo que ya puedes abrir el armario y lo que hay dentro de el.", Color.green),
                };
                break;
            case 9:
                helperLines = new (string, string, Color)[]
                {
                    ("Ewan", "Es el momento cariño, tienes que entrar en la habitación.", Color.green),
                };
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.text == helperLines[index].Item2)
            {
                NextLine();

            }
            else
            {
                StopAllCoroutines();
                textComponent.text = helperLines[index].Item2;

            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            dialog.SetActive(true);
            LoadDialoguesForScene();
            textComponent.text = string.Empty;
            helperOpen = true;
            StartDialog();
        }
    }

    void StartDialog()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    void NextLine()
    {
        if (index < helperLines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialog.SetActive(false);
            dialogFinished = true;
            helperOpen = false;
        }
    }

    IEnumerator TypeLine()
    {
        string characterName = helperLines[index].Item1;
        string dialogue = helperLines[index].Item2;
        Color textColor = helperLines[index].Item3;

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
