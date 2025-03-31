using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Lv1Dial : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isPlayerInRange;

    [SerializeField, TextArea(4,6)] private string[] dialogueLinesalph;

    [SerializeField] private GameObject Panel;


    [SerializeField] private TMP_Text DialogueText; 

    private bool didDialogueStart;

    private int LineIndex;

    private float typingTime = 0.05f;

    [SerializeField] private GameObject DialogueMark;


    void Start(){
        Panel.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if(!didDialogueStart){
                StartDialogue();
            }
            else if(DialogueText.text == dialogueLinesalph[LineIndex]){
                NextDialogueLine();
            }
            
        }
        
    }

    private void StartDialogue(){
        didDialogueStart=true;
        Panel.SetActive(true);
        DialogueMark.SetActive(false);
        LineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private IEnumerator ShowLine()
    {
        DialogueText.text = string.Empty;
        foreach(char ch in dialogueLinesalph[LineIndex])
        {   
            DialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        
        }
    }

    private void NextDialogueLine(){
        LineIndex++;
        if(LineIndex<dialogueLinesalph.Length)
        {
           StartCoroutine(ShowLine()); 
        }
        else{
            didDialogueStart = false;
            DialogueMark.SetActive(true);
            Panel.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            DialogueMark.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            DialogueMark.SetActive(false);

        }
    }

    
    
}